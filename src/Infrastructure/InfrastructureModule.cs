﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class InfrastructureModule
    {
        
        public static void ConfigureServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionString = configuration["MYSQL_CONNECTION_STRING"]
                    ?? throw new ArgumentNullException("AppDbContext connection string is required");

                System.Console.WriteLine($"Connection string: {connectionString}");

                options
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                    .EnableSensitiveDataLogging(true)
                    .EnableDetailedErrors(true);
            });

            services.AddScoped<UserRepository>();
            services.AddScoped<UserRepositorySql>();
        }
    }
}
