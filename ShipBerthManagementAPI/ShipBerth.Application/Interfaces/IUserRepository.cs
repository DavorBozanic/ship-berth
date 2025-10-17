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

        /// <summary>
        /// Gets the user by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<User?> GetUserByIdAsync(int id);

        /// <summary>
        /// Checks if user exists asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        Task<bool> UserExistsAsync(string username);

        /// <summary>
        /// Checks if email exists asynchronously.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        Task<bool> EmailExistsAsync(string email);

        /// <summary>
        /// Adds the user asynchronously.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task AddUserAsync(User user);

        /// <summary>
        /// Updates the user asynchronous.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        /// Deletes the user asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task DeleteUserAsync(int id);

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
