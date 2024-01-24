// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service.Models.Responses
{
    public record UpdateUserInfoDTO
    {
        public string? Name { get; init; }

        public byte[]? Image { get; init; }
    }
}
