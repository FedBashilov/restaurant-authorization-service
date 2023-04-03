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

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Id = Guid.NewGuid().ToString(),
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
                        Id = Guid.NewGuid().ToString(),
                        Name = Core.Constants.UserRoles.Client,
                        NormalizedName = Core.Constants.UserRoles.Client.ToUpper(),
                    });
        }
    }
}