using CrudService.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudService.Data
{
    public class DataContext : DbContext
    {
     public DataContext(DbContextOptions<DataContext> options) : base(options) {}

     public DbSet<Document> Documents { get; set; }
    }
}