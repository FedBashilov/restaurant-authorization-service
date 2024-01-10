// Copyright (c) Fedor Bashilov. All rights reserved.

namespace Authorization.Service
{
    using Authorization.Service.Models;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class MailService : IMailService
    {
        private readonly MailSettings mailSettings;

        public MailService(IOptions<MailSettings> mailSettingsOptions)
        {
            this.mailSettings = mailSettingsOptions.Value;
        }

        public async Task SendMail(MailData mailData)
        {
            using var emailMessage = new MimeMessage();
            var emailFrom = new MailboxAddress(this.mailSettings.SenderName, this.mailSettings.SenderEmail);
            emailMessage.From.Add(emailFrom);

            var emailTo = new MailboxAddress(mailData.EmailToName, mailData.EmailToId);
            emailMessage.To.Add(emailTo);

            emailMessage.Subject = mailData.EmailSubject;

            var emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = mailData.EmailBody;

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            using var mailClient = new SmtpClient();
            await mailClient.ConnectAsync(this.mailSettings.Server, this.mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await mailClient.AuthenticateAsync(this.mailSettings.UserName, this.mailSettings.Password);
            await mailClient.SendAsync(emailMessage);
            await mailClient.DisconnectAsync(true);
        }
    }
}
