// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities.Common;

namespace ShipBerth.Domain.Entities
{
    /// <summary>
    /// Docking record class.
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class DockingRecord : BaseEntity
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
        /// Gets or sets the arrival time.
        /// </summary>
        /// <value>
        /// The arrival time.
        /// </value>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Gets or sets the departure time.
        /// </summary>
        /// <value>
        /// The departure time.
        /// </value>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Gets or sets the berth.
        /// </summary>
        /// <value>
        /// The berth.
        /// </value>
        required public Berth Berth { get; set; }

        /// <summary>
        /// Gets or sets the ship.
        /// </summary>
        /// <value>
        /// The ship.
        /// </value>
        required public Ship Ship { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        required public User User { get; set; }
    }
}
