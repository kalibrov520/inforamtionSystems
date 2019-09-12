using LookupApi.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace LookupApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuccessfulItem> Items { get; set; }

        public DbSet<TransferAgentPlan> TransferAgentPlans { get; set; }
    }
}