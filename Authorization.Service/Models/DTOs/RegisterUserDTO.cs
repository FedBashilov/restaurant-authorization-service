// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    public class RegisterUserDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
