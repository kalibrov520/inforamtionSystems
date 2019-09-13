using Microsoft.EntityFrameworkCore;
using Models;

namespace DataTransformationApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<DataFeedInfo> DataFeedInfo { get; set; }
    }
}