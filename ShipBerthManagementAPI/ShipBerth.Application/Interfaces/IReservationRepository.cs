// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Reservation repository interface.
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Gets the reservation by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Reservation.</returns>
        Task<Reservation?> GetByIdAsync(int id);

        /// <summary>
        /// Gets the reservations for berth asynchronously.
        /// </summary>
        /// <param name="berthId">The berth identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>List of reservations.</returns>
        Task<List<Reservation>> GetReservationsForBerthAsync(int berthId, DateTime start, DateTime end);

        /// <summary>
        /// Adds the reservation asynchronously.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task AddReservationAsync(Reservation reservation);

        /// <summary>
        /// Updates the reservation asynchronously.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateReservationAsync(Reservation reservation);

        /// <summary>
        /// Deletes the reservation asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task DeleteReservationAsync(int id);

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SaveChangesAsync();
    }
}
