// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;
using ShipBerth.Infrastructure.Data;

namespace ShipBerth.Infrastructure.Repositories
{
    /// <summary>
    /// Reservation repository class.
    /// </summary>
    /// <seealso cref="IReservationRepository" />
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ReservationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the reservation by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Reservation.</returns>
        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await this.context.Reservations.FirstOrDefaultAsync(r => r.Id == id && r.IsDeleted == false);
        }

        /// <summary>
        /// Gets the reservations for berth asynchronously.
        /// </summary>
        /// <param name="berthId">The berth identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>List of reservations.</returns>
        public async Task<List<Reservation>> GetReservationsForBerthAsync(int berthId, DateTime start, DateTime end)
        {
            return await this.context.Reservations
                .Where(r => r.BerthId == berthId &&
                           r.ScheduledArrival <= end &&
                           r.ScheduledDeparture >= start)
                .ToListAsync();
        }

        /// <summary>
        /// Adds the reservation asynchronously.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddReservationAsync(Reservation reservation)
        {
            await this.context.Reservations.AddAsync(reservation);
        }

        /// <summary>
        /// Updates the reservation asynchronously.
        /// </summary>
        /// <param name="reservation">The reservation.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateReservationAsync(Reservation reservation)
        {
            this.context.Reservations.Update(reservation);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the reservation asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await this.context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                this.context.Reservations.Remove(reservation);
            }
        }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
