// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LogInUserDto
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
