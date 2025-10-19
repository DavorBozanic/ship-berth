// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Berth repository interface.
    /// </summary>
    public interface IBerthRepository
    {
        /// <summary>
        /// Gets the berth by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Berth.</returns>
        Task<Berth?> GetByIdAsync(int id);

        /// <summary>
        /// Gets all berths asynchronously.
        /// </summary>
        /// <returns>List of berths.</returns>
        Task<List<Berth>> GetAllAsync();

        /// <summary>
        /// Gets the available berths asynchronously.
        /// </summary>
        /// <returns>List of berths.</returns>
        Task<List<Berth>> GetAvailableBerthsAsync();

        /// <summary>
        /// Searches the berths asynchronously.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <param name="status">The status.</param>
        /// <returns>List of berths.</returns>
        Task<List<Berth>> SearchBerthsAsync(string? location, int? minSize, string? status);

        /// <summary>
        /// Adds the berth asynchronously.
        /// </summary>
        /// <param name="berth">The berth.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddBerthAsync(Berth berth);

        /// <summary>
        /// Updates the berth asynchronously.
        /// </summary>
        /// <param name="berth">The berth.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateBerthAsync(Berth berth);

        /// <summary>
        /// Deletes the berth asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteBerthAsync(int id);

        /// <summary>
        /// Saves the berth changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveChangesAsync();
    }
}
