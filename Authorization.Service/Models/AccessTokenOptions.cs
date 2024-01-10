// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public class AccessTokenOptions
    {
        public string? SecretKey { get; set; }

        public string? Issuer { get; set; }

        public string? Audience { get; set; }

        public TimeSpan TokenLifespan { get; set; }
    }
}
