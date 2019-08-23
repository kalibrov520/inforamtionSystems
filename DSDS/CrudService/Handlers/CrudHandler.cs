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
            using (var memStream = new MemoryStream(externalTask.Variables["reformatFile"].AsBytes()))
            {
                using (var package = new ExcelPackage(memStream))
                {
                    var workSheet = package.Workbook.Worksheets["PlanAsset"];

                    var documentsList = new List<Document>();

                    for (var i = 2; i <= workSheet.Dimension.Rows; i++)
                    {
                        documentsList.Add(new Document()
                        {
                            BusinessEntityName = workSheet.Cells[i, 1].Value.ToString(),
                            ISN = workSheet.Cells[i, 2].Value.ToString(),
                            FundID = workSheet.Cells[i, 3].Value.ToString(),
                            FundName = workSheet.Cells[i, 4].Value.ToString(),
                            MarketValue = float.Parse(workSheet.Cells[i, 5].Value.ToString()),
                            Shares = float.Parse(workSheet.Cells[i, 6].Value.ToString()),
                            NAV = float.Parse(workSheet.Cells[i, 7].Value.ToString()),
                            MarketDate = DateTime.Now,
                            ValidationDate = DateTime.Now
                        });
                    }

                    _repo.AddRangeAsync(documentsList);
                }
            }

            return Task.FromResult<IExecutionResult>(new CompleteResult(new Dictionary<string, Variable>()));
        }
    }
}