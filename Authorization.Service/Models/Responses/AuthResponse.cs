// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.Responses
{
    public record AuthResponse
    {
        public string? AccessToken { get; init; }

        public string? RefreshToken { get; init; }

        public string? TokenType { get; init; }

        public long ExpiresIn { get; init; }
    }
}
