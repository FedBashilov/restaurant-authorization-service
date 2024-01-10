// Copyright (c) Fedor Bashilov. All rights reserved.

using Authorization.Service.Models;

namespace Authorization.Service
{
    public interface IMailService
    {
        Task SendMail(MailData mailData);
    }
}
