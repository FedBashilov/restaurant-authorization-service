// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public record MailSettings
    {
        public string? Server { get; init; }

        public int Port { get; init; }

        public string? SenderName { get; init; }

        public string? SenderEmail { get; init; }

        public string? UserName { get; init; }

        public string? Password { get; init; }

        public string? RedirectUrl { get; init; }
    }
}
