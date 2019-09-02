using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Camunda.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FileLoaderService.Handlers
{
    [HandlerTopics("FileLoader")]
    public class FileLoaderHandler : ExternalTaskHandler
    {
        private readonly ILogger<FileLoaderHandler> _logger;

        public FileLoaderHandler(ILogger<FileLoaderHandler> logger)
        {
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                var a = JsonConvert.DeserializeObject<List<FileLoader.FileSystem.File>>(externalTask.Variables["newFiles"].AsString());

                foreach (var (variableKey, variableValue) in externalTask.Variables.Where(variable => variable.Value.Type == VariableType.Bytes))
                {
                    using (var fileWriter = new StreamWriter(".\\" + variableKey + ".bat"))
                    {
                        fileWriter.Write(System.Text.Encoding.Default.GetString(variableValue.AsBytes()));
                        _logger.LogInformation("Copy {fileName}.bat locally...", variableKey);
                    }

                    System.Diagnostics.Process.Start(variableKey + ".bat");
                    _logger.LogInformation("Starting file {fileName}.bat...", variableKey);
                }

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                {
                    ["formattedFile"] = Variable.Bytes(File.ReadAllBytes(".\\doc.xlsx"))
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing ScriptExecutorHandler");
                return Task.FromException<IExecutionResult>(ex);
            }
        }
    }
}