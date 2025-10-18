// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using ShipBerth.Application.Interfaces;
using ShipBerth.Domain.Entities;
using ShipBerth.Infrastructure.Data;

namespace ShipBerth.Infrastructure.Repositories
{
    /// <summary>
    /// User repository class.
    /// </summary>
    /// <seealso cref="IUserRepository" />
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the user by username asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await this.context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        /// <summary>
        /// Gets the user by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await this.context.Users.FindAsync(id);
        }

        /// <summary>
        /// Gets all users asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await this.context.Users.ToListAsync();
        }

        /// <summary>
        /// Checks if user exists asynchronously.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        public async Task<bool> UserExistsAsync(string username)
        {
            return await this.context.Users.AnyAsync(u => u.Username == username);
        }

        /// <summary>
        /// Checks if email exists asynchronously.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await this.context.Users.AnyAsync(u => u.Email == email);
        }

        /// <summary>
        /// Adds the user asynchronously.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddUserAsync(User user)
        {
            await this.context.Users.AddAsync(user);
        }

        /// <summary>
        /// Saves the changes asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task SaveChangesAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
