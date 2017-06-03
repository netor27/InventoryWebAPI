using System.Collections.Generic;
using System.Linq;
using Inventory.WebApi.Entities;

namespace Inventory.WebApi.Services
{
    public class ProductRepository : IProductRepository
    {
        private ProductInfoContext _context;

        public ProductRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.FirstOrDefault(c => c.Id == productId);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(c => c.Name).ToList();
        }

        public bool ProductExists(int productId)
        {
            return _context.Products.Any(x => x.Id == productId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
