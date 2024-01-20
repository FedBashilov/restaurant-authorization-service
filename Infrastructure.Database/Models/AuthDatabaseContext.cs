// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Infrastructure.Database.Models
{
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthDatabaseContext : IdentityDbContext<User>
    {
        public AuthDatabaseContext(DbContextOptions<AuthDatabaseContext> options)
            : base(options) => this.Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .HasMaxLength(250);

            //// Seed data
            var adminRoleId = Guid.NewGuid().ToString();
            var adminUserId = Guid.NewGuid().ToString();
            var clientRoleId = Guid.NewGuid().ToString();
            var clientUserId = Guid.NewGuid().ToString();

            // Roles
            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = adminRoleId,
                        Name = Core.Constants.UserRoles.Admin,
                        NormalizedName = Core.Constants.UserRoles.Admin.ToUpper(),
                    },
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = Core.Constants.UserRoles.Cook,
                        NormalizedName = Core.Constants.UserRoles.Cook.ToUpper(),
                    },
                    new IdentityRole
                    {
                        Id = clientRoleId,
                        Name = Core.Constants.UserRoles.Client,
                        NormalizedName = Core.Constants.UserRoles.Client.ToUpper(),
                    });

            // Users
            modelBuilder.Entity<User>()
                .HasData(
                    new User
                    {
                        Id = adminUserId,
                        Name = Core.Constants.UserRoles.Admin,
                        UserName = Core.Constants.UserRoles.Admin,
                        NormalizedUserName = Core.Constants.UserRoles.Admin,
                        Email = "admin@example.com",
                        NormalizedEmail = "admin@example.com",
                        PasswordHash = new PasswordHasher<User>().HashPassword(null, "!Qweqwe123"),
                        EmailConfirmed = true,
                        SecurityStamp = string.Empty,
                    },
                    new User
                    {
                        Id = clientUserId,
                        Name = Core.Constants.UserRoles.Client,
                        UserName = Core.Constants.UserRoles.Client,
                        NormalizedUserName = Core.Constants.UserRoles.Client,
                        Email = "client@example.com",
                        NormalizedEmail = "client@example.com",
                        PasswordHash = new PasswordHasher<User>().HashPassword(null, "!Qweqwe123"),
                        EmailConfirmed = true,
                        SecurityStamp = string.Empty,
                    });

            // User roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId,
                },
                new IdentityUserRole<string>
                {
                    RoleId = clientRoleId,
                    UserId = clientUserId,
                });

            // Claims
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string> // Admin claims
                {
                    Id = 1,
                    UserId = adminUserId,
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    ClaimValue = Core.Constants.UserRoles.Admin,
                },
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    UserId = adminUserId,
                    ClaimType = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor",
                    ClaimValue = adminUserId,
                },
                new IdentityUserClaim<string>
                {
                    Id = 3,
                    UserId = adminUserId,
                    ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
                    ClaimValue = "admin@example.com",
                },
                new IdentityUserClaim<string> // Client claims
                {
                    Id = 4,
                    UserId = clientUserId,
                    ClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                    ClaimValue = Core.Constants.UserRoles.Client,
                },
                new IdentityUserClaim<string>
                {
                    Id = 5,
                    UserId = clientUserId,
                    ClaimType = "http://schemas.xmlsoap.org/ws/2009/09/identity/claims/actor",
                    ClaimValue = clientUserId,
                },
                new IdentityUserClaim<string>
                {
                    Id = 6,
                    UserId = clientUserId,
                    ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
                    ClaimValue = "client@example.com",
                });
        }
    }
}