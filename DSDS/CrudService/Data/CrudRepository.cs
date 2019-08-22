using System.Collections.Generic;
using System.Threading.Tasks;
using CrudService.Models;

namespace CrudService.Data
{
    public class CrudRepository : ICrudRepository
    {
        private readonly DataContext _context;

        public CrudRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Document> documents)
        {
            await _context.PlanAsset.AddRangeAsync(documents);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<IEnumerable<Document>> GetDocumentsAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Document> GetDocumentByIdAsync(int id)
        {
            return await _context.PlanAsset.FindAsync(id);
        }
    }
}