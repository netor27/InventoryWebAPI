using Inventory.WebApi.Entities;
using System.Collections.Generic;

namespace Inventory.WebApi.Services
{
    public interface IProductCategoryRepository
    {
        ProductCategory GetProductCategory(int productCategoryId);

        IEnumerable<ProductCategory> GetProductCategories();

        void AddProductCategory(ProductCategory productCategory);

        void DeleteProductCategory(ProductCategory productCategory);

        bool ProductCategoryExists(int productCategoryId);

        bool Save();
    }
}
