// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;

    public class LogInUserDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public static bool IsValidModel(LogInUserDTO loginDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (loginDto == null)
            {
                message = "Invalid request body";
            }
            if (loginDto!.Email == null)
            {
                message = "Email is required";
            }
            if (loginDto.Password == null)
            {
                message = "Password is required";
            }

            return message == string.Empty;
        }
    }
}
