using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LookupApi.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task PostItems(TalendDocument docs)
        {
            await _context.Items.AddRangeAsync(docs.Items);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TransferAgentPlan>> GetTransferAgentPlanList()
        {
            try
            {
                return await _context.TransferAgentPlans.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}