// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;

    public record VerifyGoogleDTO
    {
        public string? Token { get; init; }

        public static bool IsValidModel(VerifyGoogleDTO dto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (dto == null)
            {
                message = "Invalid request body";
            }
            else if (dto.Token == null)
            {
                message = "Token is required";
            }

            return message == string.Empty;
        }
    }
}
