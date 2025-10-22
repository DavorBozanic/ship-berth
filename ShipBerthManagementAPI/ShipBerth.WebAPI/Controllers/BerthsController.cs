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
    /// Berths controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BerthsController : ControllerBase
    {
        private readonly IBerthService berthService;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BerthsController" /> class.
        /// </summary>
        /// <param name="berthService">The berth service.</param>
        /// <param name="logger">The logger.</param>
        public BerthsController(IBerthService berthService, ILogger<BerthsController> logger)
        {
            this.berthService = berthService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the berths.
        /// </summary>
        /// <returns>Action result with list of berths.</returns>
        [HttpGet]
        public async Task<ActionResult<List<BerthDTO>>> GetBerths()
        {
            try
            {
                var berths = await this.berthService.GetAllBerthsAsync();

                this.logger.LogInformation("Retrieved {BerthCount} berth.", berths.Count);

                return this.Ok(berths);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error fetching berth.");

                return this.BadRequest(new { message = "An error occurred while fetching berths.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the berth.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result with berth.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BerthDTO>> GetBerth(int id)
        {
            try
            {
                var berth = await this.berthService.GetBerthAsync(id);

                this.logger.LogInformation("Retrieved berth for ID: {BerthId}.", id);

                return this.Ok(berth);
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Berth not found: {BerthId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error fetching berth for ID: {BerthId}.", id);

                return this.BadRequest(new { message = "An error occurred while fetching berth.", error = ex.Message });
            }
        }

        /// <summary>
        /// Checks the availability.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>Action result with boolean.</returns>
        [HttpGet("{id}/availability")]
        public async Task<ActionResult<bool>> CheckAvailability(int id, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            try
            {
                var isAvailable = await this.berthService.IsBerthAvailableAsync(id, start, end);

                this.logger.LogInformation("Berth {BerthId} availability: {IsAvailable}.", id, isAvailable);

                return this.Ok(new { available = isAvailable });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error checking availability for berth {BerthId}.", id);

                return this.BadRequest(new { message = "An error occurred while checking availability.", error = ex.Message });
            }
        }

        /// <summary>
        /// Creates the berth.
        /// </summary>
        /// <param name="berthDto">The berth dto.</param>
        /// <returns>Action result with berth.</returns>
        [HttpPost]
        public async Task<ActionResult<BerthDTO>> CreateBerth(BerthDTO berthDto)
        {
            try
            {
                var berth = await this.berthService.CreateBerthAsync(berthDto);

                this.logger.LogInformation("Berth created successfully: {BerthId}.", berth.Id);

                return this.CreatedAtAction(nameof(this.GetBerth), new { id = berth.Id }, berth);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error creating berth with data: {@Request}.", berthDto);

                return this.BadRequest(new { message = "An error occurred while creating berth.", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the berth.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="berthDto">The berth dto.</param>
        /// <returns>Action result with berth.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<BerthDTO>> UpdateBerth(int id, BerthDTO berthDto)
        {
            try
            {
                var berth = await this.berthService.UpdateBerthAsync(id, berthDto);

                this.logger.LogInformation("Berth updated successfully: {BerthId}.", id);

                return this.Ok(berth);
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Berth not found for update: {BerthId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error updating berth {BerthId}.", id);

                return this.BadRequest(new { message = "An error occurred while updating berth.", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the berth.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBerth(int id)
        {
            try
            {
                await this.berthService.DeleteBerthAsync(id);

                this.logger.LogInformation("Berth deleted successfully: {BerthId}.", id);

                return this.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Berth not found for deletion: {BerthId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error deleting berth {BerthId}.", id);

                return this.BadRequest(new { message = "An error occurred while deleting berth.", error = ex.Message });
            }
        }
    }
}
