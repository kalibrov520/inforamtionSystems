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

        public async Task AddRangeAsync(IEnumerable<Document> documents)
        {
            await _context.AddRangeAsync(documents);

            _context.SaveChanges();
        }

    }
}