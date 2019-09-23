using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using DataTransformationApi.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataTransformationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransformationController
    {
        private readonly IDataTransformationRepository _repo;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DataTransformationController> _logger;

        public DataTransformationController(IDataTransformationRepository repo, ILogger<DataTransformationController> logger, IConfiguration configuration)
        {
            _repo = repo;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task PostDataFeedInfoAsync(DataFeedInfo info)
        {
            try
            {
                await _repo.PostDataFeedInfoAsync(info);
            }
            catch (Exception e)
            {
                _logger.LogError("PostDataFeedInfoAsync error", e);
                throw;
            }
        }

        [HttpPost("startprocess/{processDefinitionId}")]
        public void StartProcess(string processDefinitionId)
        {
            using (var client = new HttpClient())
            {
                var requestUrl = _configuration.GetSection("CamundaApi").Value + "/process-definition/key/" +
                                 processDefinitionId.Split(":").First() + "/start";
                var result = client.PostAsync(requestUrl, new StringContent(string.Empty, Encoding.UTF8, "application/json")).Result;
            }
        }

        [HttpGet]
        public async Task<IEnumerable<DataFeedMainInfo>> GetDataFeedsInfoAsync()
        {
            try
            {
                using (var client = new WebClient())
                {
                    var camundaUrl = _configuration.GetSection("CamundaApi").Value;
                    var response = client.DownloadString($"{camundaUrl}/process-definition");
                    
                    var deploymentList = JArray.Parse(response).Select(x => new
                    {
                        DataFeedId = Guid.Parse(((string) x.SelectToken("id")).Split(":").Last()),
                        ProcessDefinitionKey = (string) x.SelectToken("id"),
                        DataFeedName = (string) x.SelectToken("name")
                    }).ToList();

                    var dataFeedList = await _repo.GetDataFeedsMainInfo(deploymentList.Select(x => x.DataFeedId));
                    var result = new List<DataFeedMainInfo>();
                    
                    foreach (var info in dataFeedList)
                    {
                        var key = info.DeploymentId;
                        var element = deploymentList.FirstOrDefault(x => x.DataFeedId.Equals(key));
                        info.DataFeed = element?.DataFeedName;
                        info.DeploymentId = element.DataFeedId;
                        info.ProcessDefinitionId = element.ProcessDefinitionKey;
                        info.DataFeed = element.DataFeedName;
                        result.Add(info);
                        deploymentList.RemoveAll(x => x.DataFeedId.Equals(key));
                    }

                    foreach (var info in deploymentList)
                    {
                        result.Add(new DataFeedMainInfo()
                        {
                            DeploymentId = info.DataFeedId,
                            DataFeed = info.DataFeedName,
                            ProcessDefinitionId = info.ProcessDefinitionKey
                        });
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Get DataFeed Info error", e);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<DataFeedDetailsToReturn>> GetDataFeedDetailsInfoAsync(string id)
        {
            try
            {
                return await _repo.GetDataFeedDetails(id);
            }
            catch (Exception e)
            {
                _logger.LogError("GetDataFeedDetailsInfoAsync error", e);
                throw;
            }
        }

        [HttpGet("datafeedfailes/{runId}")]
        public async Task<IEnumerable<string>> GetDataFeedFailsAsync(string runId)
        {
            try
            {
                return await _repo.GetDataFeedFails(runId);
            }
            catch (Exception e)
            {
                _logger.LogError("GetDataFeedFailsAsync error", e);
                throw;
            }
        }


    }
}