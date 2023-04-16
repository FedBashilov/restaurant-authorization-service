// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Extentions
{
    using System.Text;
    using Authorization.Service;
    using Infrastructure.Core.Models;
    using Infrastructure.Database.Models;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.IdentityModel.Tokens;

    public static class ServicesExtentions
    {
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTokenValidationServices(configuration);
            services.AddIdentityServices(configuration);
            services.TryAddScoped<IAuthService, AuthService>();
        }

        private static void AddTokenValidationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AccessTokenOptions>(configuration.GetSection("AccessTokenOptions"));
            var secretKey = configuration.GetSection("AccessTokenOptions:SecretKey").Value ?? throw new NullReferenceException("SecretKey can not be null");
            var issuer = configuration.GetSection("AccessTokenOptions:Issuer").Value ?? throw new NullReferenceException("Issuer can not be null");
            var audience = configuration.GetSection("AccessTokenOptions:Audience").Value ?? throw new NullReferenceException("Audience can not be null");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
        }

        private static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataProtectionTokenProviderOptions>(configuration.GetSection("RefreshTokenOptions"));

            services.AddIdentityCore<User>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDatabaseContext>()
                .AddTokenProvider("UserTokenProvider", typeof(UserDataProtectorTokenProvider<User>));

            services.AddHttpContextAccessor();
            services.TryAddScoped<IUserValidator<User>, UserValidator<User>>();
            services.TryAddScoped<IPasswordValidator<User>, PasswordValidator<User>>();
            services.TryAddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<IdentityRole>, RoleValidator<IdentityRole>>();

            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<User>>();
            services.TryAddScoped<ITwoFactorSecurityStampValidator, TwoFactorSecurityStampValidator<User>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory<User, IdentityRole>>();
            services.TryAddScoped<UserManager<User>>();
            services.TryAddScoped<SignInManager<User>>();
            services.TryAddScoped<RoleManager<IdentityRole>>();
        }
    }
}
