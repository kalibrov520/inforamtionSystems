using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Camunda.Worker;
using LogSenderService.Services;
using Microsoft.Extensions.Logging;

namespace LogSenderService.Handlers
{
    [HandlerTopics("LogSender")]
    public class LogSenderHandler : ExternalTaskHandler
    {
        private readonly FailedItemsService _failedItemsService;
        private readonly ILogger<LogSenderHandler> _logger;

        public LogSenderHandler(FailedItemsService failedItemsService, ILogger<LogSenderHandler> logger)
        {
            _failedItemsService = failedItemsService;
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("", ""),
                EnableSsl = true
            };

            var from = new MailAddress("", "");
            var to = new MailAddress("");
            var m = new MailMessage(from, to) {Subject = "", Body = ""};

            smtpClient.Send(m);

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}