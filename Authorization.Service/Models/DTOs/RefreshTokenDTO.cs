// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;

    public record RefreshTokenDTO
    {
        public string? RefreshToken { get; init; }

        public string? AccessToken { get; init; }

        public static bool IsValidModel(RefreshTokenDTO tokenDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (tokenDto == null)
            {
                message = "Invalid request body";
            }
            else if (tokenDto.AccessToken == null)
            {
                message = "AccessToken is required";
            }
            else if (tokenDto.RefreshToken == null)
            {
                message = "RefreshToken is required";
            }

            return message == string.Empty;
        }
    }
}
