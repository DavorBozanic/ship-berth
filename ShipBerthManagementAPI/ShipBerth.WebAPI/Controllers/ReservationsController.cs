// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using System.Security.Claims;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController" /> class.
        /// </summary>
        /// <param name="reservationService">The reservation service.</param>
        public ReservationsController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        /// <summary>
        /// Creates the reservation.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ReservationDTO>> CreateReservation(ReservationRequestDTO request)
        {
            try
            {
                var userId = this.GetUserIdFromToken();
                var reservation = await this.reservationService.CreateReservationAsync(request, userId);

                return this.CreatedAtAction(nameof(this.GetReservation), new { id = reservation.Id }, reservation);
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while creating reservation.", error = ex.Message });
            }
        }

        /// <summary>
        /// Cancels the reservation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> CancelReservation(int id)
        {
            try
            {
                var userId = this.GetUserIdFromToken();
                await this.reservationService.CancelReservationAsync(id, userId);

                return this.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return this.Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while cancelling reservation.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the reservation.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            try
            {
                var reservation = await this.reservationService.GetReservationAsync(id);

                return this.Ok(reservation);
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while fetching reservation.", error = ex.Message });
            }
        }

        private int GetUserIdFromToken()
        {
            var userIdClaim = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new UnauthorizedAccessException("Invalid user token.");
            }

            return userId;
        }
    }
}
