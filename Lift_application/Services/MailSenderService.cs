using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using MailKit.Security;

namespace Lift_application.Services
{
    public class MailSenderService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта localhost", "info@shinegold.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
               
                await client.ConnectAsync("mail.shinegold.ru", 25, SecureSocketOptions.None);
                await client.AuthenticateAsync("info@shinegold.ru", "3627spnM_");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async void SendEmail(string email, string subject, string message)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта localhost", "info@shinegold.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {

                 await client.ConnectAsync("mail.shinegold.ru", 25, SecureSocketOptions.None);
                await client.AuthenticateAsync("info@shinegold.ru", "3627spnM_");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
