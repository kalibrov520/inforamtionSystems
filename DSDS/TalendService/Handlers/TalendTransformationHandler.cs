using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Camunda.Worker;
using CamundaUtils;
using FileLoader;
using FileLoader.FileSystem;
using FileLoader.FTP;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using TalendService.Utils;

namespace TalendService.Handlers
{
    [HandlerTopics("TalendService")]
    public class TalendTransformationHandler : CamundaTaskHandler
    {
        private readonly ILogger<TalendTransformationHandler> _logger;
        private IFileManager _fileManager;
        private string _talendUrl;
        private readonly IApiSettings _settings;

        private List<string> _savedFiles = new List<string>();
        private Dictionary<string, List<string>> _failedRecords = new Dictionary<string, List<string>>();

        private IEnumerable<IFileSystemItem> _newFiles = new List<IFileSystemItem>();


        public TalendTransformationHandler(IApiSettings settings, ILogger<TalendTransformationHandler> logger, IFileManager fileManager)
        {
            _settings = settings;
            _logger = logger;
            _fileManager = fileManager;
        }

        public override void ParseContext(ExternalTask externalTask)
        {
            _talendUrl = externalTask.Variables["url"].AsString();
            _newFiles = JsonConvert.DeserializeObject<List<File>>(externalTask.Variables["newFiles"].AsString());
            _savedFiles = JsonConvert.DeserializeObject<List<string>>(externalTask.Variables["savedFiles"].AsString());
        }
        public override async Task Process()
        {
            try
            {
                await Task.WhenAll(_savedFiles.Select(ProcessFile));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing TalendHandler");
            }
        }

        public override Task<IExecutionResult> GetExecutionResult()
        {
            //TODO: no Database service now
            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }

        private async Task ProcessFile(string filePath)
        {
            try
            {
                var failedRows = new List<string>();
                var successfulRows = new List<TalendResponseObject>();

                var file = await _fileManager.GetFileAsync(filePath);

                using (var client = new HttpClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {
                        formData.Add(new ByteArrayContent(file));

                        //TODO: deal with PostAsync andResult. Do not block Task.
                        var response = client.PostAsync(_talendUrl, formData).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = response.Content.ReadAsStringAsync().Result;

                            (successfulRows, failedRows) = TalendResponseParser.ParseTalendResponse(responseContent);

                            await client.PostAsync(_settings.LookupApiUrl,
                                new StringContent(JsonConvert.SerializeObject(successfulRows), Encoding.UTF8,
                                    "application/json"));

                            var logItem = new FileTransformationLogRecord
                            {
                                DataFeedId = DataFeedId,
                                RunId = RunId,
                                FilePath = filePath,
                                InvalidRows = failedRows
                            };

                            await client.PostAsync(_settings.DataTransformationApiUrl, new StringContent(
                                JsonConvert.SerializeObject(logItem), Encoding.UTF8,
                                "application/json"));

                        }
                        else
                        {
                            _logger.LogError("Something went wrong in reformat process! {File} transformation went wrong.", filePath);
                            throw new Exception($"{filePath} failed to transform.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}