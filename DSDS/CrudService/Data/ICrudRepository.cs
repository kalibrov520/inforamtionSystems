using System.Collections.Generic;
using System.Threading.Tasks;
using CrudService.Models;

namespace CrudService.Data
{
    public interface ICrudRepository
    {
        Task AddRangeAsync(IEnumerable<Document> documents);
    }
}