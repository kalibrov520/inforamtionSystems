using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Camunda.Worker;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace CrudService.Handlers
{
    [HandlerTopics("CrudHandler")]
    public class CrudHandler : ExternalTaskHandler
    {
        //private readonly ICrudRepository _repo;
        private readonly ILogger<CrudHandler> _logger;

        public CrudHandler(/*ICrudRepository repo,*/ ILogger<CrudHandler> logger)
        {
            //_repo = repo;
            _logger = logger;
        }

        public override Task<IExecutionResult> Process(ExternalTask externalTask)
        {
            using (var memStream = new MemoryStream())
            {
                memStream.Write(externalTask.Variables["reformatFile"].AsBytes());
                var package = new ExcelPackage(memStream);
                package.SaveAs(new FileInfo(".\\doc.xlsx"));
                
            }

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}