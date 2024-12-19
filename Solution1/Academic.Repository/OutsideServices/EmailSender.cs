using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Academic.Core.Helpers;

namespace Academic.Repository.OutsideServices
{

    public class EmailSender(IOptions<EmailConfigurationsHelper> options) : IEmailSender 
    {
        private readonly EmailConfigurationsHelper configurations = options.Value;

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Spiders", configurations.Email));
            emailMessage.To.Add(new MailboxAddress(email.Split("@")[0], email));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html") { Text = message };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(configurations.Providor, configurations.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(configurations.Email, configurations.Password);
                await client.SendAsync(emailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Email sending failed.", ex);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }

}
