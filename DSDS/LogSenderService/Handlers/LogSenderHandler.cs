using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Camunda.Worker;
using CamundaUtils;
using LogSenderService.Models;
using LogSenderService.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LogSenderService.Handlers
{
    [HandlerTopics("LogSender")]
    public class LogSenderHandler : CamundaTaskHandler
    {
        private bool _isUpdated;
        private string _email;
        private readonly SmtpService _smtpService;
        private readonly LogItemsService _logItemsService;
        private readonly ILogger<LogSenderHandler> _logger;
        private readonly IApiSettings _settings;

        public LogSenderHandler(SmtpService smtpService, LogItemsService logItemsService, ILogger<LogSenderHandler> logger, IApiSettings settings)
        {
            _smtpService = smtpService;
            _logItemsService = logItemsService;
            _logger = logger;
            _settings = settings;
        }

        public override void ParseContext(ExternalTask externalTask)
        {
            _isUpdated = externalTask.Variables["isUpdated"].AsBoolean();
            _email = externalTask.Variables["email"].AsString();
        }

        public override async Task Process()
        {
            try
            {
                if (_isUpdated)
                { 
                    using (var client = new WebClient())
                    {
                        var url = _settings.DataTransformationApiUrl;
                        var failedItems = client.DownloadString( $"{url}/api/DataTransformationLog/{RunId}");

                        var logItem = new LogItem()
                        {
                            IsSucceeded = string.IsNullOrWhiteSpace(failedItems),
                            FailedRows = JsonConvert.DeserializeObject<List<string>>(failedItems),
                            StartDate = DateTime.Now
                        };

                        if (!string.IsNullOrWhiteSpace(failedItems))
                            await _smtpService.SendEmailAsync(_email, "Error", failedItems);
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing TalendHandler");
            }
        }

        public override Task<IExecutionResult> GetExecutionResult()
        {
            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}