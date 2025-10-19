// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Reservation service interface.
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Creates the reservation asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Reservation.</returns>
        Task<ReservationDTO> CreateReservationAsync(ReservationRequestDTO request);

        /// <summary>
        /// Cancels the reservation asynchronously.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns>True or false, whether reservation is cancelled.</returns>
        Task<bool> CancelReservationAsync(int reservationId);

        /// <summary>
        /// Gets the reservation asynchronously.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns>Reservation.</returns>
        Task<ReservationDTO> GetReservationAsync(int reservationId);
    }
}
