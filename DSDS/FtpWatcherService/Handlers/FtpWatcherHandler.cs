using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;
using FileLoader;
using FileLoader.File;
using FileLoader.FileSystem;
using FileLoader.FTP;
using FtpWatcherService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

namespace FtpWatcherService.Handlers
{
    [HandlerTopics("FtpWatcher")]
    public class FtpWatcherHandler : ExternalTaskHandler
    {
        private readonly BatFileService _service;
        private readonly ILogger<FtpWatcherHandler> _logger;
        private readonly IFileWriter _fileWriter;
        private readonly IFileLoader _fileLoader;

        public FtpWatcherHandler(BatFileService service, IFileLoader fileLoader, IFileWriter fileWriter,  ILogger<FtpWatcherHandler> logger)
        {
            _service = service;
            _fileLoader = fileLoader;
            _fileWriter = fileWriter;
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                var fileLoader = new FtpFileLoader(externalTask.Variables["rootPath"].AsString(),
                    externalTask.Variables["userName"].AsString(), externalTask.Variables["password"].AsString());

                var patterns = externalTask.Variables["patternsForExtension"].AsString().Trim().Split(',').ToList();

                var previousFilesOnFtp = _fileLoader.GetFiles();

                var newFiles = fileLoader.GetFilesWithFileExtensionPattern(patterns).ToList();

                if (previousFilesOnFtp.SequenceEqual(newFiles))
                {
                    _logger.LogInformation("No new files on ftp was detected.");

                    return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                    {
                        ["isUpdated"] = Variable.Boolean(false)
                    }));
                }

                _fileWriter.WriteFilesOnFileAsync(newFiles);

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                {
                    ["downloadFile"] = Variable.Bytes(File.ReadAllBytes(".\\SS_Download.bat")),
                    ["reformatFile"] = Variable.Bytes(File.ReadAllBytes(".\\Reformat_Process.bat")),
                    ["autoloadFile"] = Variable.Bytes(File.ReadAllBytes(".\\Autoload_Process.bat")),
                    ["isUpdated"] = Variable.Boolean(true)
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing FtpWatcherHandler");

                return Task.FromResult<IExecutionResult>(new BpmnErrorResult("11",ex.ToString()));
            }
        }
    }
}