using Microsoft.EntityFrameworkCore;
using Models;

namespace LookupApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<TalendResponseObject> Items { get; set; }
    }
}