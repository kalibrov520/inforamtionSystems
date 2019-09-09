using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LogSenderService.Models;

namespace LogSenderService.Services
{
    public class SmtpService
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailAddress _fromAddress;

        public SmtpService(ISmtpSettings smtpSettings)
        {
            _smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                EnableSsl = smtpSettings.Ssl
            };

            _fromAddress = new MailAddress(smtpSettings.Username, smtpSettings.Username);
        }

        public async Task SendEmailAsync(MailMessage message)
        {
            message.From = _fromAddress;
            await _smtpClient.SendMailAsync(message);
        }
    }
}