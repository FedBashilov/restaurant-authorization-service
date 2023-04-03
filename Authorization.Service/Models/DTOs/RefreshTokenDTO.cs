// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models.DTOs
{
    using System.Diagnostics.CodeAnalysis;

    public class RefreshTokenDTO
    {
        public string? RefreshToken { get; set; }

        public string? AccessToken { get; set; }

        public static bool IsValidModel(RefreshTokenDTO tokenDto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (tokenDto == null)
            {
                message = "Invalid request body";
            }

            if (tokenDto!.AccessToken == null)
            {
                message = "AccessToken is required";
            }

            if (tokenDto.RefreshToken == null)
            {
                message = "RefreshToken is required";
            }

            return message == string.Empty;
        }
    }
}
