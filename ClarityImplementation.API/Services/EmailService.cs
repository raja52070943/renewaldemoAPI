using ClarityImplementation.API.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace ClarityImplementation.API.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(Email emailModel)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Clarity Portal", "noreply@claritybenefitsolutions.com"));
            emailMessage.To.Add(new MailboxAddress("", emailModel.ToEmail));
            emailMessage.Subject = emailModel.Subject;

            var builder = new BodyBuilder { HtmlBody = emailModel.Body };
            emailMessage.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("noreply@claritybenefitsolutions.com", "ClarityNew1$");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        public async Task<string> SendTestEmailAsync(Email emailModel)
        {
            try
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress("Clarity Portal", "noreply@claritybenefitsolutions.com"));
                emailMessage.To.Add(new MailboxAddress("", emailModel.ToEmail));
                emailMessage.Subject = emailModel.Subject;

                var builder = new BodyBuilder { HtmlBody = emailModel.Body };
                emailMessage.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("noreply@claritybenefitsolutions.com", "ClarityNew1$");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch(Exception exe)
            {
                return exe.Message;
            }
        }
    }
}
