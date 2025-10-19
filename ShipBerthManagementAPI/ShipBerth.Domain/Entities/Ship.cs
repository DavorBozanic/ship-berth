// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using ShipBerth.Domain.Entities.Common;

namespace ShipBerth.Domain.Entities
{
    /// <summary>
    /// Ship class.
    /// </summary>
    /// <seealso cref="BaseEntity" />
    public class Ship : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        [Precision(3, 2)]
        public decimal Size { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the docking records.
        /// </summary>
        /// <value>
        /// The docking records.
        /// </value>
        public ICollection<DockingRecord> DockingRecords { get; set; } = new List<DockingRecord>();

        /// <summary>
        /// Gets or sets the reservations.
        /// </summary>
        /// <value>
        /// The reservations.
        /// </value>
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
