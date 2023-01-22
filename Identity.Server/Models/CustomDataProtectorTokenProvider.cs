// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;

    public class CustomDataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
        where TUser : class
    {
        public CustomDataProtectorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<DataProtectionTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
            this.Options.TokenLifespan = TimeSpan.FromDays(30);
        }
    }
}
