// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public record AccessTokenOptions
    {
        public string? SecretKey { get; init; }

        public string? Issuer { get; init; }

        public string? Audience { get; init; }

        public TimeSpan TokenLifespan { get; init; }
    }
}
