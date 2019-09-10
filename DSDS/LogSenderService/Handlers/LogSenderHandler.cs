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
                var failedItems = externalTask.Variables["failedItems"].AsString();

                var logItem = new LogItem()
                {
                    IsSucceeded = string.IsNullOrWhiteSpace(failedItems),
                    SuccessfulItems = JsonConvert.DeserializeObject<IEnumerable<SuccessfulItem>>(externalTask.Variables["successfulItems"].AsString()),
                    FailedItems = JsonConvert.DeserializeObject<IEnumerable<FailedItem>>(failedItems),
                    StartDate = DateTime.Now
                };

                await _logItemsService.LogSingleItemAsync(logItem);

                await _smtpService.SendEmailAsync(externalTask.Variables["email"].AsString(), "Error", failedItems);
            }

            return await Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}