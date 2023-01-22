// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RefreshTokenDto
    {
        [Required]
        public string? RefreshToken { get; set; }

        [Required]
        public string? AccessToken { get; set; }
    }
}
