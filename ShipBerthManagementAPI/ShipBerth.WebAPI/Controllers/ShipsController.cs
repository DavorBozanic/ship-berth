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
    /// Ships controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShipsController : ControllerBase
    {
        private readonly IShipService shipService;
        private readonly ILogger<ShipsController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipsController" /> class.
        /// </summary>
        /// <param name="shipService">The ship service.</param>
        /// <param name="logger">The logger.</param>
        public ShipsController(IShipService shipService, ILogger<ShipsController> logger)
        {
            this.shipService = shipService;
            this.logger = logger;
        }

        /// <summary>
        /// Gets the ships.
        /// </summary>
        /// <returns>Action result with list of ships.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ShipDTO>>> GetShips()
        {
            try
            {
                var ships = await this.shipService.GetAllShipsAsync();

                this.logger.LogInformation("Retrieved {ShipCount} ships.", ships.Count);

                return this.Ok(ships);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error fetching ships.");

                return this.BadRequest(new { message = "An error occurred while fetching ships.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result with ship.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipDTO>> GetShip(int id)
        {
            try
            {
                var ship = await this.shipService.GetShipAsync(id);

                this.logger.LogInformation("Retrieved ship for ID: {ShipId}.", id);

                return this.Ok(ship);
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Ship not found: {ShipId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error fetching ship for ID: {ShipId}.", id);

                return this.BadRequest(new { message = "An error occurred while fetching ship.", error = ex.Message });
            }
        }

        /// <summary>
        /// Creates the ship.
        /// </summary>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns>Action result with ship.</returns>
        [HttpPost]
        public async Task<ActionResult<ShipDTO>> CreateShip(ShipDTO shipDto)
        {
            try
            {
                var ship = await this.shipService.CreateShipAsync(shipDto);

                this.logger.LogInformation("Ship created successfully: {ShipId}.", ship.Id);

                return this.CreatedAtAction(nameof(this.GetShip), new { id = ship.Id }, ship);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error creating ship with data: {@ShipData}.", shipDto);

                return this.BadRequest(new { message = "An error occurred while creating ship.", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns>Action result with ship.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ShipDTO>> UpdateShip(int id, ShipDTO shipDto)
        {
            try
            {
                var ship = await this.shipService.UpdateShipAsync(id, shipDto);

                this.logger.LogInformation("Ship updated successfully: {ShipId}.", id);

                return this.Ok(ship);
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Ship not found for update: {ShipId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error updating ship {ShipId}.", id);

                return this.BadRequest(new { message = "An error occurred while updating ship.", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Action result.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShip(int id)
        {
            try
            {
                await this.shipService.DeleteShipAsync(id);

                this.logger.LogInformation("Ship deleted successfully: {ShipId}.", id);

                return this.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                this.logger.LogWarning("Ship not found for deletion: {ShipId}.", id);

                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Error deleting ship {ShipId}.", id);

                return this.BadRequest(new { message = "An error occurred while deleting ship.", error = ex.Message });
            }
        }
    }
}
