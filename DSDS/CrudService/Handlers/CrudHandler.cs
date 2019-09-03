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
using Newtonsoft.Json;
using OfficeOpenXml;

namespace CrudService.Handlers
{
    [HandlerTopics("CrudHandler")]
    public class CrudHandler : ExternalTaskHandler
    {
        private readonly DataContext _context;

        private readonly ICrudRepository _repo;
        private readonly ILogger<CrudHandler> _logger;

        public CrudHandler(DataContext context, ICrudRepository repo, ILogger<CrudHandler> logger)
        {
            _context = context;
            _repo = repo;
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            try
            {
                var reformattedFiles = JsonConvert.DeserializeObject<List<Document>>(externalTask.Variables["reformattedFiles"].AsString());

                foreach (var file in reformattedFiles.Where(i => i != null))
                {
                    _context.Items.AddRange(file.Items);
                    _context.SaveChanges();
                }

                _context.AddRange(reformattedFiles);

                _logger.LogInformation("Provided Json was parsed and added to the database {json}", externalTask.Variables["reformattedFiles"].AsString());

                return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occured, while executing CrudService");
                return Task.FromResult<IExecutionResult>(new BpmnErrorResult("12", ex.ToString()));
            }
        }
    }
}