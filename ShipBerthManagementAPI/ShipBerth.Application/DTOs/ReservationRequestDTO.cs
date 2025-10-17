// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

namespace ShipBerth.Application.DTOs
{
    /// <summary>
    /// Reservation request DTO.
    /// </summary>
    public class ReservationRequestDTO
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
