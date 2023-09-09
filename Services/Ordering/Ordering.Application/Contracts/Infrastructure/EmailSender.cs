using Ordering.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Infrastructure
{
    public class EmailSender : IEmailService
    {
        public async Task<bool> SendEmail(Email email)
        {
            string SendersAddress = "hungqaz38@gmail.com";
            const string SendersPassword = "ljloagbnxunvenpi";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(SendersAddress, SendersPassword),
                Timeout = 1000
            };

            MailMessage message = new MailMessage(SendersAddress, email.To, email.Subject, email.Body);
            message.IsBodyHtml = true;
            await smtp.SendMailAsync(message);
            return true;

        }
    }
}
