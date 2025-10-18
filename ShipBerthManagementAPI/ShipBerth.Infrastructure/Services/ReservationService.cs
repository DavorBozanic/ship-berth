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
        private readonly IUserRepository userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationService"/> class.
        /// </summary>
        /// <param name="reservationRepository">The reservation repository.</param>
        /// <param name="berthRepository">The berth repository.</param>
        /// <param name="shipRepository">The ship repository.</param>
        /// <param name="userRepository">The user repository.</param>
        public ReservationService(
            IReservationRepository reservationRepository,
            IBerthRepository berthRepository,
            IShipRepository shipRepository,
            IUserRepository userRepository)
        {
            this.reservationRepository = reservationRepository;
            this.berthRepository = berthRepository;
            this.shipRepository = shipRepository;
            this.userRepository = userRepository;
        }

        /// <summary>
        /// Creates the reservation asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">
        /// Berth with ID {request.BerthId} not found.
        /// or
        /// Ship with ID {request.ShipId} not found.
        /// or
        /// User with ID {userId} not found.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Berth is not available for reservation.
        /// or
        /// Ship is too large for this berth. Berth max size: {berth.MaxShipSize}m, Ship size: {ship.Size}m.
        /// </exception>
        public async Task<ReservationDTO> CreateReservationAsync(ReservationRequestDTO request, int userId)
        {
            // Validate berth exists and is available
            var berth = await this.berthRepository.GetByIdAsync(request.BerthId);

            if (berth == null)
            {
                throw new KeyNotFoundException($"Berth with ID {request.BerthId} not found.");
            }

            if (berth.Status != BerthStatus.Available)
            {
                throw new InvalidOperationException("Berth is not available for reservation.");
            }

            // Validate ship exists
            var ship = await this.shipRepository.GetByIdAsync(request.ShipId);

            if (ship == null)
            {
                throw new KeyNotFoundException($"Ship with ID {request.ShipId} not found.");
            }

            // Validate user exists
            var user = await this.userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            // Validate berth can accommodate ship size
            if (ship.Size > berth.MaxShipSize)
            {
                throw new InvalidOperationException($"Ship is too large for this berth. Berth max size: {berth.MaxShipSize}m, Ship size: {ship.Size}m.");
            }

            // Create reservation
            var reservation = new Reservation
            {
                BerthId = request.BerthId,
                ShipId = request.ShipId,
                UserId = userId,
                ScheduledArrival = request.ScheduledArrival,
                ScheduledDeparture = request.ScheduledDeparture,
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
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Reservation with ID {reservationId} not found.</exception>
        /// <exception cref="UnauthorizedAccessException">You can only cancel your own reservations.</exception>
        public async Task<bool> CancelReservationAsync(int reservationId, int userId)
        {
            var reservation = await this.reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found");
            }

            // Check if user owns this reservation
            if (reservation.UserId != userId)
            {
                throw new UnauthorizedAccessException("You can only cancel your own reservations");
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
        /// Gets the user reservations asynchronously.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<List<ReservationDTO>> GetUserReservationsAsync(int userId)
        {
            var reservations = await this.reservationRepository.GetUserReservationsAsync(userId);

            return reservations.Select(this.MapToReservationDTO).ToList();
        }

        /// <summary>
        /// Gets the reservation asynchronously.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Reservation with ID {reservationId} not found.</exception>
        public async Task<ReservationDTO> GetReservationAsync(int reservationId)
        {
            var reservation = await this.reservationRepository.GetByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new KeyNotFoundException($"Reservation with ID {reservationId} not found");
            }

            return this.MapToReservationDTO(reservation);
        }

        private ReservationDTO MapToReservationDTO(Reservation reservation)
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
