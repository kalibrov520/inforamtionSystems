namespace LogSenderService.Models
{
    public class SmtpSettings : ISmtpSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Ssl { get; set; }
    }

    public interface ISmtpSettings
    {
        string Host { get; set; }
        int Port { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        bool Ssl { get; set; }
    }
}