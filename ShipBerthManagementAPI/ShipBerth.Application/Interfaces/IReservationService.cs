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
        /// Creates the reservation asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<ReservationDTO> CreateReservationAsync(ReservationRequestDTO request, int userId);

        /// <summary>
        /// Cancels the reservation asynchronous.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<bool> CancelReservationAsync(int reservationId, int userId);

        /// <summary>
        /// Gets the user reservations asynchronous.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        Task<List<ReservationDTO>> GetUserReservationsAsync(int userId);

        /// <summary>
        /// Gets the reservation asynchronous.
        /// </summary>
        /// <param name="reservationId">The reservation identifier.</param>
        /// <returns></returns>
        Task<ReservationDTO> GetReservationAsync(int reservationId);
    }
}