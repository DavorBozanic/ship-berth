// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

namespace ShipBerth.Application.DTOs
{
    /// <summary>
    /// Reservation DTO.
    /// </summary>
    /// <seealso cref="BaseDTO" />
    public class ReservationDTO : BaseDTO
    {
        /// <summary>
        /// Gets or sets the name of the berth.
        /// </summary>
        /// <value>
        /// The name of the berth.
        /// </value>
        public string BerthName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the ship.
        /// </summary>
        /// <value>
        /// The name of the ship.
        /// </value>
        public string ShipName { get; set; } = string.Empty;

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
    }
}
