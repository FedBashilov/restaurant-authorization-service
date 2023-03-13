// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public class RefreshTokenDto
    {
        public string? RefreshToken { get; set; }

        public string? AccessToken { get; set; }
    }
}
