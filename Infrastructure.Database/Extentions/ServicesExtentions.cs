﻿// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Extentions
{
    using Infrastructure.Core.Models;
    using Infrastructure.Database.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServicesExtentions
    {
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthDatabaseContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddIdentity<User, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDatabaseContext>()
                .AddTokenProvider("CustomTokenProvider", typeof(CustomDataProtectorTokenProvider<User>));
        }
    }
}