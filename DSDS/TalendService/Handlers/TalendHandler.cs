﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Camunda.Worker;
using FileLoader.FTP;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TalendService.Models;

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
                var url = externalTask.Variables["url"].AsString();
                //var apiUrl = externalTask.Variables["apiUrl"].AsString();

                var fileLoader = new FtpFileLoader("",
                    externalTask.Variables["userName"].AsString(), externalTask.Variables["password"].AsString());

                var filesToReformat = JsonConvert.DeserializeObject<List<FileLoader.FileSystem.File>>(externalTask.Variables["newFiles"].AsString());

                foreach (var file in filesToReformat)
                {
                    using (var client = new HttpClient())
                    {
                        using (var formData = new MultipartFormDataContent())
                        {
                            formData.Add(new ByteArrayContent(fileLoader.GetFileContent(file.FullPath)));

                            //TODO: deal with PostAsync andResult. Do not block Task.
                            var response = client.PostAsync(url, formData);

                            var responseContent = response.Result.Content.ReadAsStringAsync().Result;

                            if (response.IsCompletedSuccessfully)
                            {
                                client.PostAsync("http://localhost:59295/api/lookups", new StringContent(responseContent, Encoding.UTF8, "application/json"));
                            }
                            else
                            {
                                client.PostAsync("",
                                    new StringContent(responseContent, Encoding.UTF8, "application/json"));
                                _logger.LogError("Something went wrong in reformat process! {File} transformation went wrong.", file.Name);
                                throw new Exception($"{file.Name} failed to transform.");
                            }
                        }
                    }
                    
                }

                //TODO: no Database service now
                return Task.FromResult<IExecutionResult>(new CompleteResult());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing TalendHandler");
                return Task.FromException<IExecutionResult>(ex);
            }
        }
    }
}