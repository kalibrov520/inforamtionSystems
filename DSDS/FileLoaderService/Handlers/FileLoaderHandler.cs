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
                var filesToReformat = JsonConvert.DeserializeObject<List<FileLoader.FileSystem.File>>(externalTask.Variables["newFiles"].AsString());

                return Task.FromResult<IExecutionResult>(new CompleteResult(filesToReformat.ToDictionary(k => k.Name, v => Variable.Bytes(File.ReadAllBytes(v.FullPath)))));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing FileLoaderHandler");
                return Task.FromException<IExecutionResult>(ex);
            }
        }
    }
}