// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;
using ShipBerth.Domain.Enums;

namespace ShipBerth.Infrastructure.Services
{
    /// <summary>
    /// Reservation service class.
    /// </summary>
    /// <seealso cref="IReservationService" />
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IBerthRepository berthRepository;
        private readonly IShipRepository shipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="reservationRepository">The reservation repository.</param>
        /// <param name="berthRepository">The berth repository.</param>
        /// <param name="shipRepository">The ship repository.</param>
        public ReservationService(
            IReservationRepository reservationRepository,
            IBerthRepository berthRepository,
            IShipRepository shipRepository)
        {
            this.reservationRepository = reservationRepository;
            this.berthRepository = berthRepository;
            this.shipRepository = shipRepository;
        }

        /// <summary>
        /// Creates the reservation asynchronously.
        /// </summary>
        /// <param name="reservationRequestDto">The reservation request dto.</param>
        /// <returns>
        /// Reservation.
        /// </returns>
        /// <exception cref="KeyNotFoundException">
        /// Berth with ID {request.BerthId} not found.
        /// or
        /// Ship with ID {request.ShipId} not found.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Berth is not available for reservation.
        /// or
        /// Ship is too large for this berth. Berth max size: {berth.MaxShipSize}m, Ship size: {ship.Size}m.
        /// </exception>
        public async Task<ReservationDTO> CreateReservationAsync(ReservationRequestDTO reservationRequestDto)
        {
            // Validate berth exists and is available
            var berth = await this.berthRepository.GetByIdAsync(reservationRequestDto.BerthId);

            if (berth == null)
            {
                throw new KeyNotFoundException($"Berth with ID {reservationRequestDto.BerthId} not found.");
            }

            if (berth.Status != BerthStatus.Available)
            {
                throw new InvalidOperationException("Berth is not available for reservation.");
            }

            // Validate ship exists
            var ship = await this.shipRepository.GetByIdAsync(reservationRequestDto.ShipId);

            if (ship == null)
            {
                throw new KeyNotFoundException($"Ship with ID {reservationRequestDto.ShipId} not found.");
            }

            // Validate berth can accommodate ship size
            if (ship.Size > berth.MaxShipSize)
            {
                throw new InvalidOperationException($"Ship is too large for this berth. Berth max size: {berth.MaxShipSize}m, Ship size: {ship.Size}m.");
            }

            // Create reservation
            var reservation = new Reservation
            {
                BerthId = reservationRequestDto.BerthId,
                ShipId = reservationRequestDto.ShipId,
                UserId = reservationRequestDto.UserId,
                ScheduledArrival = reservationRequestDto.ScheduledArrival,
                ScheduledDeparture = reservationRequestDto.ScheduledDeparture,
            };

            await this.reservationRepository.AddReservationAsync(reservation);
            await this.reservationRepository.SaveChangesAsync();

            // Update berth status to Reserved
            berth.Status = BerthStatus.Reserved;
            await this.berthRepository.UpdateBerthAsync(berth);
            await this.berthRepository.SaveChangesAsync();

            return await this.GetReservationAsync(reservation.Id);
        }

        /// <summary>
        /// Cancels the reservation asynchronously.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns>True or false, whether reservation is cancelled.</returns>
        /// <exception cref="KeyNotFoundException">Reservation with ID {reservationId} not found.</exception>
        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservation = await this.reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");
            }

            // Update reservation status
            await this.reservationRepository.UpdateReservationAsync(reservation);
            await this.reservationRepository.SaveChangesAsync();

            // Update berth status back to Available
            var berth = await this.berthRepository.GetByIdAsync(reservation.BerthId);

            if (berth != null)
            {
                berth.Status = BerthStatus.Available;
                await this.berthRepository.UpdateBerthAsync(berth);
                await this.berthRepository.SaveChangesAsync();
            }

            return true;
        }

        /// <summary>
        /// Gets the reservation asynchronously.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns>Reservation.</returns>
        /// <exception cref="KeyNotFoundException">Reservation with ID {reservationId} not found.</exception>
        public async Task<ReservationDTO> GetReservationAsync(int reservationId)
        {
            var reservation = await this.reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found.");
            }

            return MapToReservationDTO(reservation);
        }

        private static ReservationDTO MapToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO
            {
                Id = reservation.Id,
                BerthName = reservation.Berth?.Name ?? "Unknown",
                ShipName = reservation.Ship?.Name ?? "Unknown",
                ScheduledArrival = reservation.ScheduledArrival,
                ScheduledDeparture = reservation.ScheduledDeparture,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt,
                IsDeleted = reservation.IsDeleted,
            };
        }
    }
}
