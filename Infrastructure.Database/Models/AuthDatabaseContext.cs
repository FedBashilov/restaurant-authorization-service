// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Infrastructure.Database.Models
{
    using Infrastructure.Core.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthDatabaseContext : IdentityDbContext<User>
    {
        public AuthDatabaseContext(DbContextOptions<AuthDatabaseContext> options)
            : base(options) => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(e => e.Name)
                .HasMaxLength(250);
        }
    }
}