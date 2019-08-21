using System.Collections.Generic;
using System.Threading.Tasks;
using CrudService.Models;

namespace CrudService.Data
{
    public interface ICrudRepository
    {
        Task Add<T>(T entity) where T : class;

        Task Delete<T>(T entity) where T : class;

        Task<bool> SaveAll();

        Task<IEnumerable<ReformattedDocument>> GetDocumentsAsync();

        Task<ReformattedDocument> GetDocumentByIdAsync(int id);
    }
}