using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace LookupApi.Data
{
    public interface ILookupRepository
    {
        Task PostItems(IEnumerable<TalendResponseObject> docs);

        Task<List<TransferAgentPlan>> GetTransferAgentPlanList();
    }
}