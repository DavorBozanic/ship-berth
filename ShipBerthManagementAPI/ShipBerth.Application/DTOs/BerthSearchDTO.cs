// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

namespace ShipBerth.Application.DTOs
{
    /// <summary>
    /// Berth search DTO.
    /// </summary>
    public class BerthSearchDTO
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public string? Location { get; set; }

        /// <summary>
        /// Gets or sets the minimum size.
        /// </summary>
        /// <value>
        /// The minimum size.
        /// </value>
        public int? MinSize { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string? Status { get; set; }
    }
}
