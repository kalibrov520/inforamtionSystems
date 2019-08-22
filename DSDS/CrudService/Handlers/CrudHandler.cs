using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Camunda.Worker;
using CrudService.Data;
using CrudService.Models;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace CrudService.Handlers
{
    [HandlerTopics("CrudHandler")]
    public class CrudHandler : ExternalTaskHandler
    {
        private readonly ICrudRepository _repo;
        private readonly ILogger<CrudHandler> _logger;

        public CrudHandler(ICrudRepository repo, ILogger<CrudHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            using (var memStream = new MemoryStream(externalTask.Variables["reformatFile"].AsBytes()))
            {
                using (var package = new ExcelPackage(memStream))
                {

                }
            }

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}