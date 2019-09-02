using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Worker;
using Microsoft.Extensions.Logging;

namespace TalendService.Handlers
{
    [HandlerTopics("TalendService")]
    public class TalendHandler : ExternalTaskHandler
    {
        private readonly ILogger<TalendHandler> _logger;

        public TalendHandler(ILogger<TalendHandler> logger)
        {
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                foreach (var (variableKey, variableValue) in externalTask.Variables.Where(variable => variable.Value.Type == VariableType.Bytes))
                {
                    using (var fileWriter = new StreamWriter(".\\" + variableKey + ".xlsx"))
                    {
                        fileWriter.Write(System.Text.Encoding.Default.GetString(variableValue.AsBytes()));
                        _logger.LogInformation("Copy {fileName} locally...", variableKey);
                    }
                }

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing TalendHandler");
                return Task.FromException<IExecutionResult>(ex);
            }
        }
    }
}