// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    public class RefreshTokenDTO
    {
        public string? RefreshToken { get; set; }

        public string? AccessToken { get; set; }
    }
}
