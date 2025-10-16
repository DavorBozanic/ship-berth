// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities.Common;

namespace ShipBerth.Domain.Entities
{
    /// <summary>
    /// Reservation class.
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class Reservation : BaseEntity
    {
        /// <summary>
        /// Gets or sets the berth identifier.
        /// </summary>
        /// <value>
        /// The berth identifier.
        /// </value>
        public int BerthId { get; set; }

        /// <summary>
        /// Gets or sets the ship identifier.
        /// </summary>
        /// <value>
        /// The ship identifier.
        /// </value>
        public int ShipId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the scheduled arrival.
        /// </summary>
        /// <value>
        /// The scheduled arrival.
        /// </value>
        public DateTime ScheduledArrival { get; set; }

        /// <summary>
        /// Gets or sets the scheduled departure.
        /// </summary>
        /// <value>
        /// The scheduled departure.
        /// </value>
        public DateTime ScheduledDeparture { get; set; }

        /// <summary>
        /// Gets or sets the berth.
        /// </summary>
        /// <value>
        /// The berth.
        /// </value>
        public Berth Berth { get; set; } = null!;

        /// <summary>
        /// Gets or sets the ship.
        /// </summary>
        /// <value>
        /// The ship.
        /// </value>
        public Ship Ship { get; set; } = null!;

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; } = null!;
    }
}
