using System.Collections.Generic;
using System.Threading.Tasks;
using LookupApi.Models;

namespace LookupApi.Data
{
    public interface ILookupRepository
    {
        Task PostItems(TalendDocument docs);
    }
}