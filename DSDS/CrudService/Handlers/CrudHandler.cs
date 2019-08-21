using System.Collections.Generic;
using System.Threading.Tasks;
using Camunda.Worker;
using CrudService.Data;
using Microsoft.Extensions.Logging;

namespace CrudService.Handlers
{
    [HandlerTopics("Crud")]
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
            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}