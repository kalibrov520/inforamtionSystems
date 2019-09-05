using LookupApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LookupApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<SuccessfulItem> Items { get; set; }
    }
}