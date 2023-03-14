// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service
{
    using Microsoft.AspNetCore.DataProtection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class UserDataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser>
        where TUser : class
    {
        public UserDataProtectorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<DataProtectionTokenProviderOptions> options,
            ILogger<DataProtectorTokenProvider<TUser>> logger)
            : base(dataProtectionProvider, options, logger)
        {
        }
    }
}
