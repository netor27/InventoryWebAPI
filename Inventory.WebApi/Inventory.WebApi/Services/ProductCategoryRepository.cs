using System.Collections.Generic;
using System.Linq;
using Inventory.WebApi.Entities;


namespace Inventory.WebApi.Services
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private ProductInfoContext _context;

        public ProductCategoryRepository(ProductInfoContext context)
        {
            _context = context;
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
        }

        public void DeleteProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Remove(productCategory);
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _context.ProductCategories.OrderBy(x => x.Name).ToList();
        }

        public ProductCategory GetProductCategory(int productCategoryId)
        {
            return _context.ProductCategories.FirstOrDefault(x => x.Id == productCategoryId);
        }

        public bool ProductCategoryExists(int productCategoryId)
        {
            return _context.ProductCategories.Any(x => x.Id == productCategoryId);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
