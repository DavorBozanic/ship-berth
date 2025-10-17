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
    /// Ship repository class.
    /// </summary>
    /// <seealso cref="IShipRepository" />
    public class ShipRepository : IShipRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ShipRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the ship by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Ship?> GetByIdAsync(int id)
        {
            return await this.context.Ships.FindAsync(id);
        }

        /// <summary>
        /// Gets all ships asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Ship>> GetAllAsync()
        {
            return await this.context.Ships.ToListAsync();
        }

        /// <summary>
        /// Gets the ships by type asynchronous.
        /// </summary>
        /// <param name="shipType">Type of the ship.</param>
        /// <returns></returns>
        public async Task<List<Ship>> GetShipsByTypeAsync(string shipType)
        {
            return await this.context.Ships
                .Where(s => s.Type.ToString() == shipType)
                .ToListAsync();
        }

        /// <summary>
        /// Adds the ship asynchronously.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddShipAsync(Ship ship)
        {
            await this.context.Ships.AddAsync(ship);
        }

        /// <summary>
        /// Updates the ship asynchronously.
        /// </summary>
        /// <param name="ship">The ship.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpdateShipAsync(Ship ship)
        {
            this.context.Ships.Update(ship);

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the ship asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task DeleteShipAsync(int id)
        {
            var ship = await this.context.Ships.FindAsync(id);

            if (ship != null)
            {
                this.context.Ships.Remove(ship);
            }
        }

        /// <summary>
        /// Saves the ship changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
