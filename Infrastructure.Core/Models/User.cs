// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Infrastructure.Core.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
