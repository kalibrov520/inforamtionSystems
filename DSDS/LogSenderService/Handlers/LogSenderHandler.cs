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
        private readonly SmtpService _smtpService;
        private readonly LogItemsService _logItemsService;
        private readonly ILogger<LogSenderHandler> _logger;

        public LogSenderHandler(SmtpService smtpService, LogItemsService logItemsService, ILogger<LogSenderHandler> logger)
        {
            _smtpService = smtpService;
            _logItemsService = logItemsService;
            _logger = logger;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            var from = new MailAddress("test@gmail.com", "test");
            var to = new MailAddress("test@emergn.com");
            var m = new MailMessage(from, to) {Subject = "test", Body = "test"};

            await _smtpService.SendEmailAsync(m);

            return await Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}