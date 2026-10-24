using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Server.Application.Configurations;
using Server.Application.Interfaces.Service;

namespace Server.Infrastructure
{
    public class EmailService(IOptions<EmailConfiguration> emailConfiguration) : IEmailService
    {
        private readonly EmailConfiguration _emailConfiguration = emailConfiguration.Value;

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_emailConfiguration.Name, _emailConfiguration.Email));

            message.To.Add(
                MailboxAddress.Parse(to));

            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailConfiguration.Host,
                _emailConfiguration.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailConfiguration.Email,
                _emailConfiguration.Password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
