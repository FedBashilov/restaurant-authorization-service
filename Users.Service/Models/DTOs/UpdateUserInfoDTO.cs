// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service.Models.Responses
{
    using System.Diagnostics.CodeAnalysis;

    public record UpdateUserInfoDTO
    {
        public string? Name { get; init; }

        public byte[]? Image { get; init; }

        public static bool IsValidModel(UpdateUserInfoDTO dto, [NotNullWhen(false)] out string message)
        {
            message = string.Empty;

            if (dto == null)
            {
                message = "Invalid request body";
            }

            return message == string.Empty;
        }
    }
}
