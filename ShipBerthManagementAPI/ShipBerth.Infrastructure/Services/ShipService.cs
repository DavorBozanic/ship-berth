// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;

namespace ShipBerth.Infrastructure.Services
{
    /// <summary>
    /// Ship service class.
    /// </summary>
    /// <seealso cref="IShipService" />
    public class ShipService : IShipService
    {
        private readonly IShipRepository shipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipService"/> class.
        /// </summary>
        /// <param name="shipRepository">The ship repository.</param>
        public ShipService(IShipRepository shipRepository)
        {
            this.shipRepository = shipRepository;
        }

        /// <summary>
        /// Gets all ships asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShipDTO>> GetAllShipsAsync()
        {
            var ships = await this.shipRepository.GetAllAsync();

            return ships.Select(this.MapToShipDTO).ToList();
        }

        /// <summary>
        /// Gets the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Ship with ID {id} not found.</exception>
        public async Task<ShipDTO> GetShipAsync(int id)
        {
            var ship = await this.shipRepository.GetByIdAsync(id);

            if (ship == null)
            {
                throw new KeyNotFoundException($"Ship with ID {id} not found.");
            }

            return this.MapToShipDTO(ship);
        }

        /// <summary>
        /// Creates the ship asynchronously.
        /// </summary>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns></returns>
        public async Task<ShipDTO> CreateShipAsync(ShipDTO shipDto)
        {
            var ship = new Ship
            {
                Name = shipDto.Name,
                Size = shipDto.Size,
                Type = shipDto.Type,
            };

            await this.shipRepository.AddShipAsync(ship);
            await this.shipRepository.SaveChangesAsync();

            return this.MapToShipDTO(ship);
        }

        /// <summary>
        /// Updates the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException">Ship with ID {id} not found.</exception>
        public async Task<ShipDTO> UpdateShipAsync(int id, ShipDTO shipDto)
        {
            var existingShip = await this.shipRepository.GetByIdAsync(id);

            if (existingShip == null)
            {
                throw new KeyNotFoundException($"Ship with ID {id} not found.");
            }

            existingShip.Name = shipDto.Name;
            existingShip.Size = shipDto.Size;
            existingShip.Type = shipDto.Type;
            existingShip.UpdatedAt = DateTime.UtcNow;

            await this.shipRepository.UpdateShipAsync(existingShip);
            await this.shipRepository.SaveChangesAsync();

            return this.MapToShipDTO(existingShip);
        }

        /// <summary>
        /// Deletes the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<bool> DeleteShipAsync(int id)
        {
            await this.shipRepository.DeleteShipAsync(id);
            await this.shipRepository.SaveChangesAsync();

            return true;
        }

        private ShipDTO MapToShipDTO(Ship ship)
        {
            return new ShipDTO
            {
                Id = ship.Id,
                Name = ship.Name,
                Size = ship.Size,
                Type = ship.Type,
                CreatedAt = ship.CreatedAt,
                UpdatedAt = ship.UpdatedAt,
                IsDeleted = ship.IsDeleted,
            };
        }
    }
}
