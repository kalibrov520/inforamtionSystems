using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using LogSenderService.Models;

namespace LogSenderService.Services
{
    public class SmtpService
    {
        private readonly SmtpClient _smtpClient;

        public SmtpService(ISmtpSettings smtpSettings)
        {
            _smtpClient = new SmtpClient(smtpSettings.Host, smtpSettings.Port)
            {
                Credentials = new NetworkCredential(smtpSettings.Username, smtpSettings.Password),
                EnableSsl = smtpSettings.Ssl
            };
        }

        public async Task SendEmailAsync(MailMessage message) 
            =>  await _smtpClient.SendMailAsync(message);

    }
}