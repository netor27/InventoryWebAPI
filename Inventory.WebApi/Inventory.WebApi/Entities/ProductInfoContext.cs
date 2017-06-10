using Microsoft.EntityFrameworkCore;

namespace Inventory.WebApi.Entities
{
    public class ProductInfoContext : DbContext
    {
        public ProductInfoContext(DbContextOptions<ProductInfoContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}