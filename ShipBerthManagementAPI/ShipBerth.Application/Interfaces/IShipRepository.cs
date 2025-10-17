// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Ship repository interface.
    /// </summary>
    public interface IShipRepository
    {
        /// <summary>
        /// Gets the ship by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Ship?> GetByIdAsync(int id);

        /// <summary>
        /// Gets all ships asynchronously.
        /// </summary>
        /// <returns></returns>
        Task<List<Ship>> GetAllAsync();

        /// <summary>
        /// Adds the ship asynchronously.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns></returns>
        Task AddShipAsync(Ship ship);

        /// <summary>
        /// Updates the ship asynchronously.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns></returns>
        Task UpdateShipAsync(Ship ship);

        /// <summary>
        /// Deletes the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteShipAsync(int id);

        /// <summary>
        /// Saves the ship changes asynchronously.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
