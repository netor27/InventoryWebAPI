using Inventory.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Entities
{
    public class ProductInfoContext : DbContext
    { 
        public ProductInfoContext(DbContextOptions<ProductInfoContext> options)
           : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}