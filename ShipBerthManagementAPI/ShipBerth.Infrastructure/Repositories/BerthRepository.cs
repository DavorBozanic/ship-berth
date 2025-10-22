// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;
using ShipBerth.Domain.Enums;
using ShipBerth.Infrastructure.Data;

namespace ShipBerth.Infrastructure.Repositories
{
    /// <summary>
    /// Berth repository class.
    /// </summary>
    /// <seealso cref="IBerthRepository" />
    public class BerthRepository : IBerthRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BerthRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BerthRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the berth by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Berth.</returns>
        public async Task<Berth?> GetByIdAsync(int id)
        {
            return await this.context.Berths
                .Include(b => b.Reservations)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        /// <summary>
        /// Gets all berths asynchronously.
        /// </summary>
        /// <returns>List of berths.</returns>
        public async Task<List<Berth>> GetAllAsync()
        {
            return await this.context.Berths.Where(b => b.IsDeleted == false).ToListAsync();
        }

        /// <summary>
        /// Gets the available berths asynchronously.
        /// </summary>
        /// <returns>List of berths.</returns>
        public async Task<List<Berth>> GetAvailableBerthsAsync()
        {
            return await this.context.Berths
                .Where(b => b.Status == BerthStatus.Available && b.IsDeleted == false)
                .ToListAsync();
        }

        /// <summary>
        /// Searches the berths asynchronously.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="minSize">The minimum size.</param>
        /// <param name="status">The status.</param>
        /// <returns>List of berths.</returns>
        public async Task<List<Berth>> SearchBerthsAsync(string? location, int? minSize, string? status)
        {
            var query = this.context.Berths.AsQueryable();

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(b => b.Location.Contains(location));
            }

            if (minSize.HasValue)
            {
                query = query.Where(b => b.MaxShipSize >= minSize.Value);
            }

            if (!string.IsNullOrEmpty(status) && Enum.TryParse<BerthStatus>(status, out var statusEnum))
            {
                query = query.Where(b => b.Status == statusEnum);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Adds the berth asynchronously.
        /// </summary>
        /// <param name="berth">The berth.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddBerthAsync(Berth berth)
        {
            await this.context.Berths.AddAsync(berth);
        }

        /// <summary>
        /// Updates the berth asynchronously.
        /// </summary>
        /// <param name="berth">The berth.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateBerthAsync(Berth berth)
        {
            this.context.Berths.Update(berth);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the berth asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteBerthAsync(int id)
        {
            var berth = await this.context.Berths.FindAsync(id);

            if (berth != null)
            {
                this.context.Berths.Remove(berth);
            }
        }

        /// <summary>
        /// Saves the berth changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
