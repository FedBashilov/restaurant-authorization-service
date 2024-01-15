// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service.Models
{
    public record MailData
    {
        public string? EmailToId { get; init; }

        public string? EmailToName { get; init; }

        public string? EmailSubject { get; init; }

        public string? EmailBody { get; init; }
    }
}
