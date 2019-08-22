using System.Collections.Generic;
using System.Threading.Tasks;
using CrudService.Models;

namespace CrudService.Data
{
    public interface ICrudRepository
    {
        Task Add<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        Task AddRangeAsync(IEnumerable<Document> documents);

        Task<bool> SaveAll();

        Task<IEnumerable<Document>> GetDocumentsAsync();

        Task<Document> GetDocumentByIdAsync(int id);
    }
}