// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.Responses
{
    public class AuthResponse
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }

        public string? TokenType { get; set; }

        public long ExpiresIn { get; set; }
    }
}
