// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities;
using ShipBerth.Domain.Enums;

namespace ShipBerth.Application.DTOs
{
    /// <summary>
    /// Berth DTO.
    /// </summary>
    /// <seealso cref="BaseDTO" />
    public class BerthDTO : BaseDTO
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
        /// Gets or sets the maximum size of the ship.
        /// </summary>
        /// <value>
        /// The maximum size of the ship.
        /// </value>
        public int MaxShipSize { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public BerthStatus Status { get; set; }

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
