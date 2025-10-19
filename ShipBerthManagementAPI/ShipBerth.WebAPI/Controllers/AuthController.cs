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
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController" /> class.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="logger">The logger.</param>
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            this.authService = authService;
            this.logger = logger;
        }

        /// <summary>
        /// Logins the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Action result with login response.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO request)
        {
            try
            {
                var result = await this.authService.LoginAsync(request);

                this.logger.LogInformation("Login successful for user: {Username}.", request.Username);

                return this.Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                this.logger.LogWarning("Login failed - invalid credentials for user: {Username}.", request.Username);

                return this.Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "Unexpected error during login for user: {Username}.", request.Username);

                return this.BadRequest(new { message = "An error occurred during login.", error = ex.Message });
            }
        }

        /// <summary>
        /// Registers the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Action result with register response.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<RegisterResponseDTO>> Register(RegisterRequestDTO request)
        {
            try
            {
                var result = await this.authService.RegisterAsync(request);

                if (!result.Success)
                {
                    this.logger.LogWarning("Registration failed for user: {Username} - {Message}.", request.Username, result.Message);

                    return this.BadRequest(result);
                }

                this.logger.LogInformation("Registration successful for user: {Username}.", request.Username);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
               this.logger.LogInformation("Unexpected error during registration for user: {Username}.", request.Username);

               return this.BadRequest(new { message = "An error occurred during registration.", error = ex.Message });
            }
        }
    }
}
