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

        /// <summary>
        /// Initializes a new instance of the <see cref="ShipsController"/> class.
        /// </summary>
        /// <param name="shipService">The ship service.</param>
        public ShipsController(IShipService shipService)
        {
            this.shipService = shipService;
        }

        /// <summary>
        /// Gets the ships.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ShipDTO>>> GetShips()
        {
            try
            {
                var ships = await this.shipService.GetAllShipsAsync();

                return this.Ok(ships);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while fetching ships.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ShipDTO>> GetShip(int id)
        {
            try
            {
                var ship = await this.shipService.GetShipAsync(id);

                return this.Ok(ship);
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while fetching ship details.", error = ex.Message });
            }
        }

        /// <summary>
        /// Creates the ship.
        /// </summary>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ShipDTO>> CreateShip(ShipDTO shipDto)
        {
            try
            {
                var ship = await this.shipService.CreateShipAsync(shipDto);

                return this.CreatedAtAction(nameof(this.GetShip), new { id = ship.Id }, ship);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while creating ship.", error = ex.Message });
            }
        }

        /// <summary>
        /// Updates the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="shipDto">The ship dto.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ShipDTO>> UpdateShip(int id, ShipDTO shipDto)
        {
            try
            {
                var ship = await this.shipService.UpdateShipAsync(id, shipDto);

                return this.Ok(ship);
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while updating ship.", error = ex.Message });
            }
        }

        /// <summary>
        /// Deletes the ship.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShip(int id)
        {
            try
            {
                await this.shipService.DeleteShipAsync(id);

                return this.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while deleting ship.", error = ex.Message });
            }
        }
    }
}
