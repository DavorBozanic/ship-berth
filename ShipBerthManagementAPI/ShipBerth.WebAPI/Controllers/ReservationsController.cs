// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;

namespace ShipBerth.WebAPI.Controllers
{
    /// <summary>
    /// Reservations controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService reservationService;
        private readonly ILogger<ReservationsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController" /> class.
        /// </summary>
        /// <param name="reservationService">The reservation service.</param>
        /// <param name="logger">The logger.</param>
        public ReservationsController(IReservationService reservationService, ILogger<ReservationsController> logger)
        {
            this.reservationService = reservationService;
            this.logger = logger;
        }

        /// <summary>
        /// Creates the reservation.
        /// </summary>
        /// <param name="reservationRequestDto">The reservation request dto.</param>
        /// <returns>Action result with reservation.</returns>
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationRequestDTO reservationRequestDto)
        {
            try
            {
                var reservation = await this.reservationService.CreateReservationAsync(reservationRequestDto);

                this.logger.LogInformation("Reservation created successfully: {ReservationId}.", reservation.Id);

                return this.CreatedAtAction(nameof(this.GetReservation), new { id = reservation.Id }, reservation);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error creating reservation with data: {@ReservationData}.", reservationRequestDto);

                return this.BadRequest(new { message = "An error occurred while creating reservation.", error = ex.Message });
            }
        }

        /// <summary>
        /// Cancels the reservation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelReservation(int id)
        {
            try
            {
                await this.reservationService.CancelReservationAsync(id);

                this.logger.LogInformation("Reservation cancelled successfully: {ReservationId}.", id);

                return this.NoContent();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error cancelling reservation {ReservationId}.", id);

                return this.BadRequest(new { message = "An error occurred while cancelling reservation.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the reservation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result with reservation.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            try
            {
                var reservation = await this.reservationService.GetReservationAsync(id);

                this.logger.LogInformation("Retrieved reservation for ID: {ReservationId}.", id);

                return this.Ok(reservation);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error fetching reservation for ID: {ReservationId}.", id);

                return this.BadRequest(new { message = "An error occurred while fetching reservation.", error = ex.Message });
            }
        }
    }
}
