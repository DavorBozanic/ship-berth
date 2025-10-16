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
        /// Gets the by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Reservation?> GetByIdAsync(int id);
        /// <summary>
        /// Gets the user reservations asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<Reservation>> GetUserReservationsAsync(int userId);

        /// <summary>
        /// Gets the reservations for berth asynchronous.
        /// </summary>
        /// <param name="berthId">The berth identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        Task<List<Reservation>> GetReservationsForBerthAsync(int berthId, DateTime start, DateTime end);

        /// <summary>
        /// Adds the reservation asynchronous.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <returns></returns>
        Task AddReservationAsync(Reservation reservation);

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
