// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.AspNetCore.Mvc;
using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;

namespace ShipBerth.WebAPI.Controllers
{
    /// <summary>
    /// Authentication controller.
    /// </summary>
    /// <seealso cref="ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            try
            {
                var result = await this.authService.LoginAsync(request);

                return this.Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return this.Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred during login.", error = ex.Message });
            }
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterRequestDTO request)
        {
            try
            {
                var result = await this.authService.RegisterAsync(request);

                if (!result.Success)
                {
                    return this.BadRequest(result);
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { message = "An error occurred during registration.", error = ex.Message });
            }
        }
    }
}
