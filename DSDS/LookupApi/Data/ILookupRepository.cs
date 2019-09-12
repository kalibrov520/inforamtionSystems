using System.Collections.Generic;
using System.Threading.Tasks;
using LookupApi.Models;
using Models;

namespace LookupApi.Data
{
    public interface ILookupRepository
    {
        Task PostItems(TalendDocument docs);

        Task<List<TransferAgentPlan>> GetTransferAgentPlanList();
    }
}