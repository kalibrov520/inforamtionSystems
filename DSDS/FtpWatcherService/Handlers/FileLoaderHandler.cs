using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Camunda.Worker;
using CamundaUtils;
using FileLoader;
using FileLoader.FileSystem;
using FileLoader.FTP;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Models;
using Newtonsoft.Json;
using File = System.IO.File;

namespace FtpWatcherService.FileLoader
{
    [HandlerTopics("FtpWatcher")]
    public class FileLoaderHandler : CamundaTaskHandler
    {
        private readonly ILogger<FileLoaderHandler> _logger;
        private readonly IFileWriter _fileWriter;
        private readonly IFileLoader _fileLoader;
        private readonly IFileChecker _fileChecker;
        private readonly IFileManager _fileManager;

        private bool _isUpdated;

        private List<string> _savedFiles = new List<string>();

        private IEnumerable<IFileSystemItem> _newFiles = new List<IFileSystemItem>();

        private IFileLoader _dataFeedFileLoader;
        
        private string RootPath { get; set; }

        private string UserName { get; set; }

        private string Password { get; set; }

        private List<string> Patterns { get; set; }



        public FileLoaderHandler(IFileChecker fileChecker, IFileManager fileManager, ILogger<FileLoaderHandler> logger)
        {
            _fileManager = fileManager;
            _fileChecker = fileChecker;
            _logger = logger;
        }

        public override void ParseContext(ExternalTask externalTask)
        {
            RootPath = externalTask.Variables["rootPath"].AsString();
            UserName = externalTask.Variables["userName"].AsString();
            Password = externalTask.Variables["password"].AsString();
            Patterns = externalTask.Variables["patternsForExtension"].AsString().Trim().Split(',').ToList();

            RunId = Guid.NewGuid();

            _dataFeedFileLoader = new FtpFileLoader(RootPath, UserName, Password);
        }
        
        public override async Task Process()
        {
            try
            {
                var filesOnFtp = _dataFeedFileLoader.GetFilesWithFileExtensionPattern(Patterns).ToList();

                _newFiles = _fileChecker.GetNewFileList(filesOnFtp).ToList();

                await Task.WhenAll(_newFiles.Select(TaskUploadNewFile));

                await _fileChecker.WriteNewFilesOnFileAsync(_newFiles);

                await LogFileReading();

                _isUpdated = _savedFiles.Any();

                if (!_isUpdated)
                {
                    _logger.LogInformation("No new files on ftp was detected.");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing FtpWatcherHandler");
                throw;
            }
        }

        public override Task<IExecutionResult> GetExecutionResult()
        {
            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()
            {
                ["newFiles"] = Variable.String(JsonConvert.SerializeObject(_newFiles)),
                ["savedFiles"] = Variable.String(JsonConvert.SerializeObject(_savedFiles)),
                ["runId"] = Variable.String(RunId.ToString()),
                ["isUpdated"] = Variable.Boolean(_isUpdated)
            }));
        }

        private async void UploadNewFiles()
        {
            await Task.WhenAll(_newFiles.Select(TaskUploadNewFile));
        }

        public async Task TaskUploadNewFile(IFileSystemItem file)
        {
            var safeFilePath = Regex.Replace(file.FullPath.Replace(RootPath, ""), "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]", "_");
            var savedPath = await _fileManager.SaveFileAsync(DataFeedId, RunId, safeFilePath, _dataFeedFileLoader.GetFileContent(file.FullPath));
            _savedFiles.Add(savedPath);
        }

        private async Task LogFileReading()
        {
            var logItem = new FileReadingLogRecord
            {
                DataFeedId = DataFeedId,
                RunId = RunId,
                FilePathList = _savedFiles
            };
            using (var client = new HttpClient()) 
            {
                await client.PostAsync("http://localhost:49691/api/datafeedrunlog", new StringContent(
                    JsonConvert.SerializeObject(logItem), Encoding.UTF8,
                    "application/json"));
            }
        }

    }
}