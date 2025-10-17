// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Application.DTOs;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// Auth service interface.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Logins asynchronously.
        /// </summary>
        /// <param name="loginRequest">The login request.</param>
        /// <returns>
        /// A task that completes with <see cref="LoginResponseDTO"/> when authentication finishes.
        /// - On success: Contains valid token and user data.
        /// - On failure: Contains error message and failure reason.
        /// </returns>
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest);

        /// <summary>
        /// Registers asynchronously.
        /// </summary>
        /// <param name="registerRequest">The register request.</param>
        /// <returns>
        /// A task that completes with <see cref="RegisterResponseDTO"/> when registration finishes.
        /// - On success: Contains user ID and confirmation message.
        /// - On failure: Contains error messages for duplicate email, weak password, etc.
        /// </returns>
        Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO registerRequest);

        /// <summary>
        /// Checks asynchronously if user exists.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A task that completes with bool when registration finishes.
        /// - On success: true.
        /// - On failure: false.
        /// </returns>
        Task<bool> UserExistsAsync(string username);

        /// <summary>
        /// Checks asynchronously if email exists.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// A task that completes with bool when registration finishes.
        /// - On success: true.
        /// - On failure: false.
        /// </returns>
        Task<bool> EmailExistsAsync(string email);
    }
}
