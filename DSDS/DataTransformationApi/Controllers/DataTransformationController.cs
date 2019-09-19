using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                //ignored
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

                    // TODO convert deploymetnId to GUID

                    var deploymentList = JArray.Parse(response).Select(x => new
                    {
                        DataFeedId = Guid.Parse(((string) x.SelectToken("id")).Split(":").Last()),
                        DataFeedName = (string) x.SelectToken("name")
                    }).ToDictionary(x => x.DataFeedId, y => y.DataFeedName);

                    var dataFeedList = await _repo.GetDataFeedsMainInfo(deploymentList.Keys.ToList());
                    var result = new List<DataFeedMainInfo>();

                    foreach (var info in dataFeedList)
                    {
                        var key = info.DeploymentId;
                        info.DataFeed = deploymentList[key];
                        deploymentList.Remove(key);
                        result.Add(info);
                    }
                    foreach (var (key, value) in deploymentList)
                    {
                        result.Add(new DataFeedMainInfo
                        {
                            DeploymentId = key,
                            DataFeed = value
                        });
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Get DataFeed Info error");
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
                Console.WriteLine(e);
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
                Console.WriteLine(e);
                throw;
            }
        }
    }
}