using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace DataTransformationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataFeedRunLogController : ControllerBase
    {
        private readonly IDataTransformationRepository _repo;
        private readonly ILogger<DataFeedRunLogController> _logger;

        public DataFeedRunLogController(IDataTransformationRepository repo, ILogger<DataFeedRunLogController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task Post(FileReadingLogRecord logItem)
        {
            try
            {
                await _repo.LogFileReading(logItem);
            }
            catch (Exception e)
            {
                _logger.LogError("Writing Data Feed Run Log error:{0}", e.Message);
            }
            
        }

    }
}