using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Camunda.Worker;
using FileLoader.FileSystem;
using FileLoader.FTP;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

namespace FtpWatcherService.Handlers
{
    [HandlerTopics("FtpWatcher")]
    public class FtpWatcherHandler : ExternalTaskHandler
    {
        private readonly ILogger<FtpWatcherHandler> _logger;

        public FtpWatcherHandler(ILogger<FtpWatcherHandler> logger)
        {
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                var fileLoader = new FtpFileLoader(externalTask.Variables["rootPath"].AsString(),
                    externalTask.Variables["userName"].AsString(), externalTask.Variables["password"].AsString());

                var patterns = externalTask.Variables["patternsForExtension"].AsString().Trim().Split(',').ToList();

                var filesOnFtp = new List<IFileSystemItem>();

                using (var textReader = new StreamReader(".\\FilesList.txt"))
                {
                    while (!textReader.EndOfStream)
                    {
                        var lineProperties = textReader.ReadLine()?.Split(' ');
                        filesOnFtp.Add(new FileLoader.FileSystem.File
                        {
                            Name = lineProperties?[0],
                            FullPath = lineProperties?[1],
                            LastModified =
                                Convert.ToDateTime(lineProperties?[2] + " " + lineProperties?[3] + lineProperties?[4])
                        });
                        _logger.LogInformation("File on Ftp from last session: {fullPath}, {lastModified}", lineProperties[1], lineProperties[2] + " " + lineProperties[3] + lineProperties[4]);
                    }
                }

                var newFiles = fileLoader.GetFilesWithFileExtensionPattern(patterns).ToList();

                if (filesOnFtp.SequenceEqual(newFiles))
                {
                    _logger.LogInformation("No new files on ftp was detected.");

                    return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                    {
                        ["isUpdated"] = Variable.Boolean(false)
                    }));
                }
                    

                using (var textWriter = new StreamWriter(".\\FilesList.txt"))
                {
                    foreach (var item in newFiles)
                    {
                        var lineToWrite = item.Name + " " + item.FullPath + " " + item.LastModified.ToString("g");
                        textWriter.WriteLineAsync(lineToWrite);

                        _logger.LogInformation("Updates FilesList with new file on ftp: {fullPath}, {lastModified}", item.FullPath, item.LastModified.ToString("g"));
                    }
                }

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
                {
                    ["downloadFile"] = Variable.Bytes(File.ReadAllBytes("C:\\Users\\ikalibrov\\Desktop\\SS_Download.bat")),
                    ["reformatFile"] = Variable.Bytes(File.ReadAllBytes("C:\\Users\\ikalibrov\\Desktop\\Reformat_Process.bat")),
                    ["autoloadFile"] = Variable.Bytes(File.ReadAllBytes("C:\\Users\\ikalibrov\\Desktop\\Autoload_Process.bat")),
                    ["isUpdated"] = Variable.Boolean(true)
                }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing FtpWatcherHandler");

                return Task.FromResult<IExecutionResult>(new FailureResult(ex));
            }
        }
    }
}