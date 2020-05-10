using DaHo.M151.DataFormatValidator.Models.StorageModels;
using Microsoft.EntityFrameworkCore;

namespace DaHo.M151.DataFormatValidator.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
        }

        public DbSet<DataSchema> DataSchemas { get; set; }
    }
}
