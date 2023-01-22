// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Extentions
{
    using System.Text;
    using Identity.Server.Models;
    using Identity.Server.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;
    using Web.Facade.Models;

    public static class ServicesExtentions
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
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

            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            var secretKey = configuration.GetSection("JWTSettings:SecretKey").Value ?? throw new NullReferenceException("SecretKey can not be null");
            var issuer = configuration.GetSection("JWTSettings:Issuer").Value ?? throw new NullReferenceException("Issuer can not be null");
            var audience = configuration.GetSection("JWTSettings:Audience").Value ?? throw new NullReferenceException("Audience can not be null");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.FromSeconds(10),
                };
            });
            services.TryAddScoped<IAuthService, AuthService>();
        }
    }
}
