// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public class RegisterUserDto
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
