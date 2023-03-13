// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Infrastructure.Database
{
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
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
        }
    }
}
