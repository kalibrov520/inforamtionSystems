using System.Reflection.Metadata;
using CrudService.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudService.Data
{
    public class DataContext : DbContext
    {
     public DataContext(DbContextOptions<DataContext> options) : base(options) {}

     public DbSet<ReformattedDocument> Documents { get; set; }
    }
}