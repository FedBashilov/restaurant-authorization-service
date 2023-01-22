// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Identity.Server.Models
{
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string? Name { get; set; }
    }
}
