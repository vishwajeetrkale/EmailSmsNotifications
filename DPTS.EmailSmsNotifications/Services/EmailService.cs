﻿// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
using DPTS.EmailSmsNotifications.IServices;
using DPTS.EmailSmsNotifications.ServiceModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace DPTS.EmailSmsNotifications.Services
{
    public class EmailService : IEmailService
    {
        private ConfigService _config;

        public EmailService()
        {
            _config = new ConfigService();

        }

        public async Task SendEmail(EmailModel model)
        {
            string apiKey = _config.SendGridAPIKey;
            dynamic sg = new SendGridAPIClient(apiKey);

            Email from = new Email(model.from);
            //string subject = "Sending with SendGrid is Fun";
            string subject = model.subject;
            Email to = new Email(model.to);
            //Content content = new Content("text/plain", "and easy to do anywhere, even with C#");
            Content content = new Content("text/plain", model.content);
            Mail mail = new Mail(from, subject, to, content);

            dynamic response = await sg.client.mail.send.post(requestBody: mail.Get());
        }
    }
}