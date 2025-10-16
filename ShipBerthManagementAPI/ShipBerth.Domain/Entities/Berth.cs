// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities.Common;
using ShipBerth.Domain.Enums;

namespace ShipBerth.Domain.Entities
{
    /// <summary>
    /// Berth class.
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class Berth : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string Location { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the maximum meters size of the ship.
        /// </summary>
        /// <value>
        /// The maximum size of the ship.
        /// </value>
        public int MaxMetersShipSize { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BerthStatus Status { get; set; }
    }
}
