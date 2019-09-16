using DataTransformationApi.DataModels;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DataTransformationApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<DataFeedInfo> SuccessfulRows { get; set; }

        public DbSet<DataFeedRunLog> DataFeedRunLog { get; set; }

        public DbSet<DataFeedFileLoadingLog> DataFeedFileLoadingLog { get; set; }

        public DbSet<DataTransformationLog> DataTransformationLog { get; set; }
    }
}