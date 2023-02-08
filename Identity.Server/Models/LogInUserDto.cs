﻿// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LogInUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
