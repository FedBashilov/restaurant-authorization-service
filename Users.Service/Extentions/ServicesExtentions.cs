// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service.Extentions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    public static class ServicesExtentions
    {
        public static void AddUserServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddScoped<IUserService, UserService>();
        }
    }
}
