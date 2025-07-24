using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using ServicesAbstraction;
using Share.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MailServices(IOptions<MailSettings> _options) : IMailServices
    {
        public async Task SendEmailAsync(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Value.Email),
                Subject = email.subject
            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.Value.Email, _options.Value.DisplayName));
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

           await  smtp.ConnectAsync(
                _options.Value.Host,
                _options.Value.Port,
                MailKit.Security.SecureSocketOptions.StartTls
                );
           await smtp.AuthenticateAsync(_options.Value.Email , _options.Value.Password);

           await smtp.SendAsync( mail );

           await smtp.DisconnectAsync(true);
        }

    }
}
