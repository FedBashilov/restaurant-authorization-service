// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;
    using System.Net.Mail;

    public class RegisterUserDTO
    {
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public static bool IsValidModel(RegisterUserDTO userDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (userDto == null)
            {
                message = "Invalid request body";
            }

            if (userDto!.Email == null)
            {
                message = "Email is required";
            }

            if (!MailAddress.TryCreate(userDto.Email!, out _))
            {
                message = "Invalid Email";
            }

            if (userDto.Password == null)
            {
                message = "Password is required";
            }

            if (userDto.Password!.Length < 8)
            {
                message = "Password must be at least 8 characters";
            }

            return message == string.Empty;
        }
    }
}
