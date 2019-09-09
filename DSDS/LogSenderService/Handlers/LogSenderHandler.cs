using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Camunda.Worker;
using LogSenderService.Models;
using LogSenderService.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
            if (!externalTask.Variables["isUpdated"].AsBoolean())
            {
                await _logItemsService.LogSingleItemAsync(new LogItem()
                {
                    IsSucceeded = true,
                    StartDate = DateTime.Now
                });
            }
            else
            {
                var isSucceeded = string.IsNullOrWhiteSpace(externalTask.Variables["failedItems"].AsString());

                var logItem = new LogItem()
                {
                    IsSucceeded = isSucceeded,
                    SuccessfulItems = JsonConvert.DeserializeObject<IEnumerable<SuccessfulItem>>(externalTask.Variables["successfulItems"].AsString()),
                    FailedItems = isSucceeded ? null : JsonConvert.DeserializeObject<IEnumerable<FailedItem>>(externalTask.Variables["failedItems"].AsString()),
                    StartDate = DateTime.Now
                };

                await _logItemsService.LogSingleItemAsync(logItem);

                var message = new MailMessage() { Subject = "test", Body = logItem.FailedItems.ToString(), To = { new MailAddress(externalTask.Variables["mailAddress"].AsString()) } };

                await _smtpService.SendEmailAsync(message);
            }

            return await Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}