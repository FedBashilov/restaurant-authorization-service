// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RefreshTokenDto
    {
        public string? RefreshToken { get; set; }

        public string? AccessToken { get; set; }
    }
}
