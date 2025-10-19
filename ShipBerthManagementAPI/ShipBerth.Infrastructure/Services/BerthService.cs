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
    /// Berth service class.
    /// </summary>
    /// <seealso cref="IBerthService" />
    public class BerthService : IBerthService
    {
        private readonly IBerthRepository berthRepository;
        private readonly IReservationRepository reservationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BerthService"/> class.
        /// </summary>
        /// <param name="berthRepository">The berth repository.</param>
        /// <param name="reservationRepository">The reservation repository.</param>
        public BerthService(IBerthRepository berthRepository, IReservationRepository reservationRepository)
        {
            this.berthRepository = berthRepository;
            this.reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Gets all berths asynchronously.
        /// </summary>
        /// <returns>List of berths.</returns>
        public async Task<List<BerthDTO>> GetAllBerthsAsync()
        {
            var berths = await this.berthRepository.GetAllAsync();

            return berths.Select(this.MapToBerthDTO).ToList();
        }

        /// <summary>
        /// Searches the berths asynchronously.
        /// </summary>
        /// <param name="searchDto">The search dto.</param>
        /// <returns>List of berths.</returns>
        public async Task<List<BerthDTO>> SearchBerthsAsync(BerthSearchDTO searchDto)
        {
            var berths = await this.berthRepository.SearchBerthsAsync(
                searchDto.Location,
                searchDto.MinSize,
                searchDto.Status);

            return berths.Select(this.MapToBerthDTO).ToList();
        }

        /// <summary>
        /// Gets the berth asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Berth.</returns>
        /// <exception cref="KeyNotFoundException">Berth with ID {id} not found.</exception>
        public async Task<BerthDetailDTO> GetBerthAsync(int id)
        {
            var berth = await this.berthRepository.GetByIdAsync(id);

            if (berth == null)
            {
                throw new KeyNotFoundException($"Berth with ID {id} not found.");
            }

            return new BerthDetailDTO
            {
                Id = berth.Id,
                Name = berth.Name,
                Location = berth.Location,
                MaxShipSize = berth.MaxShipSize,
                Status = berth.Status,
                CreatedAt = berth.CreatedAt,
                UpdatedAt = berth.UpdatedAt,
            };
        }

        /// <summary>
        /// Creates the berth asynchronously.
        /// </summary>
        /// <param name="berthDto">The berth dto.</param>
        /// <returns>Berth.</returns>
        public async Task<BerthDTO> CreateBerthAsync(BerthDTO berthDto)
        {
            var berth = new Berth
            {
                Name = berthDto.Name,
            };

            await this.berthRepository.AddBerthAsync(berth);
            await this.berthRepository.SaveChangesAsync();

            return this.MapToBerthDTO(berth);
        }

        /// <summary>
        /// Updates the berth asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="berthDto">The berth dto.</param>
        /// <returns>Berth.</returns>
        /// <exception cref="KeyNotFoundException">Berth with ID {id} not found.</exception>
        public async Task<BerthDTO> UpdateBerthAsync(int id, BerthDTO berthDto)
        {
            var existingBerth = await this.berthRepository.GetByIdAsync(id);

            if (existingBerth == null)
            {
                throw new KeyNotFoundException($"Berth with ID {id} not found.");
            }

            existingBerth.Name = berthDto.Name;
            existingBerth.Location = berthDto.Location;
            existingBerth.MaxShipSize = berthDto.MaxShipSize;
            existingBerth.Status = berthDto.Status;
            existingBerth.Reservations = berthDto.Reservations;
            existingBerth.DockingRecords = berthDto.DockingRecords;
            existingBerth.UpdatedAt = DateTime.UtcNow;

            await this.berthRepository.UpdateBerthAsync(existingBerth);
            await this.berthRepository.SaveChangesAsync();

            return this.MapToBerthDTO(existingBerth);
        }

        /// <summary>
        /// Determines whether [is berth available asynchronously] [the specified berth identifier].
        /// </summary>
        /// <param name="berthId">The berth identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>True or false, whether berth is available.</returns>
        public async Task<bool> IsBerthAvailableAsync(int berthId, DateTime start, DateTime end)
        {
            var berth = await this.berthRepository.GetByIdAsync(berthId);

            if (berth == null || berth.Status != BerthStatus.Available)
            {
                return false;
            }

            var conflictingReservations = await this.reservationRepository.GetReservationsForBerthAsync(berthId, start, end);

            return conflictingReservations.Count == 0;
        }

        private BerthDTO MapToBerthDTO(Berth berth)
        {
            return new BerthDTO
            {
                Id = berth.Id,
                Name = berth.Name,
                Location = berth.Location,
                MaxShipSize = berth.MaxShipSize,
                Status = berth.Status,
                CreatedAt = berth.CreatedAt,
                UpdatedAt = berth.UpdatedAt,
            };
        }
    }
}
