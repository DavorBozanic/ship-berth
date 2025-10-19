// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Ship service interface.
    /// </summary>
    public interface IShipService
    {
        /// <summary>
        /// Gets all ships asynchronously.
        /// </summary>
        /// <returns>List of ships.</returns>
        Task<List<ShipDTO>> GetAllShipsAsync();

        /// <summary>
        /// Gets the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Ship.</returns>
        Task<ShipDTO> GetShipAsync(int id);

        /// <summary>
        /// Creates the ship asynchronously.
        /// </summary>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns>Ship.</returns>
        Task<ShipDTO> CreateShipAsync(ShipDTO shipDto);

        /// <summary>
        /// Updates the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns>Ship.</returns>
        Task<ShipDTO> UpdateShipAsync(int id, ShipDTO shipDto);

        /// <summary>
        /// Deletes the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>True or false, whether ship is deleted.</returns>
        Task<bool> DeleteShipAsync(int id);
    }
}
