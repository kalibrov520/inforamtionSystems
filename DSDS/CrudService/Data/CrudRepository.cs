using System.Collections.Generic;
using System.Threading.Tasks;
using CrudService.Models;

namespace CrudService.Data
{
    public class CrudRepository : ICrudRepository
    {
        public Task Add<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task Delete<T>(T entity) where T : class
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> SaveAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ReformattedDocument>> GetDocumentsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<ReformattedDocument> GetDocumentByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}