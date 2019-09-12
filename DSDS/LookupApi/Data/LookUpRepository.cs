using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace LookupApi.Data
{
    public class LookUpRepository : ILookupRepository
    {
        private readonly DataContext _context;

        public LookUpRepository(DataContext context)
        {
            _context = context;
        }

        public async Task PostItems(IEnumerable<TalendResponseObject> docs)
        {
            await _context.Items.AddRangeAsync(docs);

            await _context.SaveChangesAsync();
        }
    }
}