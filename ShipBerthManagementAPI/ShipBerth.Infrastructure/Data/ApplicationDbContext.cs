// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using ShipBerth.Domain.Entities;

namespace ShipBerth.Infrastructure.Data
{
    /// <summary>
    /// Application database context.
    /// </summary>
    /// <seealso cref="DbContext" />
    /// <remarks>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </remarks>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public DbSet<User> Users => this.Set<User>();

        /// <summary>
        /// Gets the ships.
        /// </summary>
        /// <value>
        /// The ships.
        /// </value>
        public DbSet<Ship> Ships => this.Set<Ship>();

        /// <summary>
        /// Gets the berths.
        /// </summary>
        /// <value>
        /// The berths.
        /// </value>
        public DbSet<Berth> Berths => this.Set<Berth>();

        /// <summary>
        /// Gets the docking records.
        /// </summary>
        /// <value>
        /// The docking records.
        /// </value>
        public DbSet<DockingRecord> DockingRecords => this.Set<DockingRecord>();

        /// <summary>
        /// Gets the reservations.
        /// </summary>
        /// <value>
        /// The reservations.
        /// </value>
        public DbSet<Reservation> Reservations => this.Set<Reservation>();

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run. However, it will still run when creating a compiled model.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
        /// examples.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Berth>()
                .HasIndex(b => b.Name)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
