// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;

    public record LogInUserDTO
    {
        public string? Email { get; init; }

        public string? Password { get; init; }

        public static bool IsValidModel(LogInUserDTO loginDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (loginDto == null)
            {
                message = "Invalid request body";
            }
            else if (loginDto.Email == null)
            {
                message = "Email is required";
            }
            else if (loginDto.Password == null)
            {
                message = "Password is required";
            }

            return message == string.Empty;
        }
    }
}
