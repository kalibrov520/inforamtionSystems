using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransformationApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;

namespace DataTransformationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataTransformationLogController : ControllerBase
    {
        private readonly IDataTransformationRepository _repo;
        private readonly ILogger<DataTransformationLogController> _logger;

        public DataTransformationLogController(IDataTransformationRepository repo, ILogger<DataTransformationLogController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        // POST api/values
        [HttpPost]
        public async Task Post(FileTransformationLogRecord logItem)
        {
            await _repo.LogDataTransformation(logItem);

            await _repo.LogFileTotalRows(logItem);
        }

        [HttpGet("{runId}")]
        public IEnumerable<string> GetFailedRowsByRunId(string runId)
        {
            return _repo.GetFailedRowsByRunId(runId);
        }
    }
}