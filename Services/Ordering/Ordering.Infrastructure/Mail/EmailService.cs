using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public ILogger<EmailService> _logger { get; }
        public IConfiguration _configuration { get; }
        public EmailService( ILogger<EmailService> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            EmailSettings emailSettings = new EmailSettings
            {
                FromAddress= _configuration.GetSection("EmailSettings")["FromAddress"]!,
                Password= _configuration.GetSection("EmailSettings")["Password"]!
            };
            SmtpClient smtp = new SmtpClient
            {
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(emailSettings.FromAddress, emailSettings.Password),
                Timeout = 1000
            };

            MailMessage message = new MailMessage(emailSettings.FromAddress, email.To, email.Subject, email.Body);
            message.IsBodyHtml = true;
            await smtp.SendMailAsync(message);
            return true;

        }
    }
}
