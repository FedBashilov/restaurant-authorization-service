// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Users.Service.Models.Responses
{
    public record UserResponse
    {
        public string? Id { get; init; }

        public string? Name { get; init; }

        public string? Email { get; init; }
    }
}
