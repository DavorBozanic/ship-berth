// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using ShipBerth.Application.DTOs;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;

namespace ShipBerth.Application.Services
{
    /// <summary>
    /// Auth service.
    /// </summary>
    /// <seealso cref="IAuthService" />
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Hashes the password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>Base64 string.</returns>
        private static string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        /// <summary>
        /// Logins the asynchronous.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        /// <exception cref="System.UnauthorizedAccessException">Invalid credentials.</exception>
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            var user = await this.context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            if (user.PasswordHash != HashPassword(request.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDTO
            {
                Token = token,
                Username = user.Username,
                Role = user.Role,
                UserId = user.Id,
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequestDTO request)
        {
            if (await UserExistsAsync(request.Username))
            {
                throw new InvalidOperationException("Username already exists");
            }

            if (await EmailExistsAsync(request.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = HashPassword(request.Password),
                Role = "User",
            };

            this.context.Users.Add(user);

            return await this.context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await this.context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await this.context.Users.AnyAsync(u => u.Email == email);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                this.configuration["Jwt:Key"] ?? "YourSuperSecretKeyHereWithSufficientLength"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
