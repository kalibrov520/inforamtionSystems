using System;
using System.Threading.Tasks;
using Camunda.Worker;
using LogService.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace LogService.Handlers
{
    [HandlerTopics("LogService")]
    public class LogHandler : ExternalTaskHandler
    {
        private readonly Services.LogService _service;
        private readonly ILogger<LogHandler> _logger;

        public LogHandler(Services.LogService service, ILogger<LogHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public override async Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                if (!externalTask.Variables["isUpdated"].AsBoolean())
                {
                    await _service.LogSingleItem(new LogItem()
                    {
                        IsSucceeded = true,
                        StartDate = DateTime.Now
                    });
                }
            }
            catch (Exception e)
            {
                //ignore
            }
            return await Task.FromResult<IExecutionResult>(new CompleteResult());
        }
    }
}