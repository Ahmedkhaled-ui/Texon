using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Texon.Domin.Entities.Products;

namespace Texon.Persistence.Context
{
    public class TexonContext : DbContext
    {
        public TexonContext(DbContextOptions<TexonContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
    }
}
