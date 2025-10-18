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

        /// <summary>
        /// Initializes a new instance of the <see cref="BerthsController"/> class.
        /// </summary>
        /// <param name="berthService">The berth service.</param>
        public BerthsController(IBerthService berthService)
        {
            this.berthService = berthService;
        }

        /// <summary>
        /// Gets the berths.
        /// </summary>
        /// <param name="searchDto">The search dto.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BerthDTO>>> GetBerths([FromQuery] BerthSearchDTO searchDto)
        {
            try
            {
                List<BerthDTO> berths;

                if (HasSearchParameters(searchDto))
                {
                    berths = await this.berthService.SearchBerthsAsync(searchDto);
                }
                else
                {
                    berths = await this.berthService.GetAllBerthsAsync();
                }

                return this.Ok(berths);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while fetching berths.", error = ex.Message });
            }
        }

        /// <summary>
        /// Gets the berth.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BerthDetailDTO>> GetBerth(int id)
        {
            try
            {
                var berth = await this.berthService.GetBerthDetailAsync(id);

                return this.Ok(berth);
            }
            catch (KeyNotFoundException ex)
            {
                return this.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while fetching berth details.", error = ex.Message });
            }
        }

        /// <summary>
        /// Checks the availability.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        [HttpGet("{id}/availability")]
        public async Task<ActionResult<bool>> CheckAvailability(int id, [FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            try
            {
                var isAvailable = await this.berthService.IsBerthAvailableAsync(id, start, end);

                return this.Ok(new { available = isAvailable });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while checking availability.", error = ex.Message });
            }
        }

        /// <summary>
        /// Creates the berth.
        /// </summary>
        /// <param name="berthDto">The berth dto.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ShipDTO>> CreateBerth(BerthDTO berthDto)
        {
            try
            {
                var berth = await this.berthService.CreateBerthAsync(berthDto);

                return this.CreatedAtAction(nameof(this.GetBerth), new { id = berth.Id }, berth);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred while creating berth.", error = ex.Message });
            }
        }

        private static bool HasSearchParameters(BerthSearchDTO searchDto)
        {
            return !string.IsNullOrEmpty(searchDto.Location) ||
                   searchDto.MinSize.HasValue ||
                   !string.IsNullOrEmpty(searchDto.Status);
        }
    }
}
