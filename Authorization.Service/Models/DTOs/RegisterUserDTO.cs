// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public record RegisterUserDTO
    {
        public string? Name { get; init; }

        public string? Email { get; init; }

        public string? Password { get; init; }

        public static bool IsValidModel(RegisterUserDTO userDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (userDto == null)
            {
                message = "Invalid request body";
            }
            else if (userDto.Email == null)
            {
                message = "Email is required";
            }
            else if (!MailAddress.TryCreate(userDto.Email, out _))
            {
                message = "Invalid Email";
            }
            else if (userDto.Password == null)
            {
                message = "Password is required";
            }
            else if (userDto.Password.Length < 8)
            {
                message = "Password must be at least 8 characters";
            }

            return message == string.Empty;
        }
    }
}
