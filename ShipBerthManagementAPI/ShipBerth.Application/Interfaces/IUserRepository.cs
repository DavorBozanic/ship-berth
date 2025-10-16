// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using ShipBerth.Domain.Entities;

namespace ShipBerth.Application.Interfaces
{
    /// <summary>
    /// User repository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets the user by username asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> UserExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
        Task AddUserAsync(User user);
        Task SaveChangesAsync();
    }
}
