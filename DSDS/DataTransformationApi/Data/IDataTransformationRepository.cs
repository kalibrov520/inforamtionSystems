using System.Threading.Tasks;
using Models;

namespace DataTransformationApi.Data
{
    public interface IDataTransformationRepository
    {
        Task PostDataFeedInfoAsync(DataFeedInfo info);
    }
}