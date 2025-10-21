// Copyright (c) Maritime Center of Excellence d.o.o.. All rights reserved.
// CONFIDENTIAL; Property of Maritime Center of Excellence d.o.o.
// Unauthorized reproduction, copying, distribution or any other use of the whole or any part of this documentation/data/software is strictly prohibited.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipBerth.Application.Interfaces;
using ShipBerth.Infrastructure.Data;
using ShipBerth.Infrastructure.Repositories;
using ShipBerth.Infrastructure.Services;

namespace ShipBerth.Infrastructure
{
    /// <summary>
    /// Dependency injection class.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Adds the infrastructure.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBerthRepository, BerthRepository>();
            services.AddScoped<IShipRepository, ShipRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBerthService, BerthService>();
            services.AddScoped<IShipService, ShipService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
