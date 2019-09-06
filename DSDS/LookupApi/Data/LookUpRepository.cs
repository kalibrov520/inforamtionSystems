using System.Collections.Generic;
using System.Threading.Tasks;
using LookupApi.Models;

namespace LookupApi.Data
{
    public class LookUpRepository : ILookupRepository
    {
        private readonly DataContext _context;

        public LookUpRepository(DataContext context)
        {
            _context = context;
        }

        public async Task PostItems(TalendDocument docs)
        {
            await _context.Items.AddRangeAsync(docs.Items);

            await _context.SaveChangesAsync();
        }
    }
}