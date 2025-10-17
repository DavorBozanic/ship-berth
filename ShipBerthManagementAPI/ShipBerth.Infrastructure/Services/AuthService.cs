// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;

namespace ShipBerth.Infrastructure.Services
{
    /// <summary>
    /// Auth service class.
    /// </summary>
    /// <seealso cref="IAuthService" />
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="configuration">The configuration.</param>
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.configuration = configuration;
        }

        /// <summary>
        /// Logins asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="UnauthorizedAccessException">Invalid credentials.</exception>
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await this.userRepository.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Simple password check for now - replace with proper hashing later
            if (user.PasswordHash != HashPassword(request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            var token = this.GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                UserId = user.Id,
            };
        }

        /// <summary>
        /// Registers asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<RegisterResponseDTO> RegisterAsync(RegisterRequestDTO request)
        {
            if (await this.userRepository.UserExistsAsync(request.Username))
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Message = "Username already exists.",
                };
            }

            if (await this.userRepository.EmailExistsAsync(request.Email))
            {
                return new RegisterResponseDTO
                {
                    Success = false,
                    Message = "Email already exists.",
                };
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password), // Simple hash for now
                Role = "User",
            };

            await this.userRepository.AddUserAsync(user);
            await this.userRepository.SaveChangesAsync();

            return new RegisterResponseDTO
            {
                Success = true,
                Message = "User registered successfully.",
                Username = user.Username,
                UserId = user.Id,
            };
        }

        /// <summary>
        /// Checks asynchronously if user exists.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>
        /// A task that completes with bool when registration finishes.
        /// - On success: true.
        /// - On failure: false.
        /// </returns>
        public async Task<bool> UserExistsAsync(string username)
        {
            return await this.userRepository.UserExistsAsync(username);
        }

        /// <summary>
        /// Checks asynchronously if email exists.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// A task that completes with bool when registration finishes.
        /// - On success: true.
        /// - On failure: false.
        /// </returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await this.userRepository.EmailExistsAsync(email);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"] ?? "YourSuperSecretKeyHereWithSufficientLength"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: this.configuration["Jwt:Issuer"],
                audience: this.configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
