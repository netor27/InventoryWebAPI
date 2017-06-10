using Inventory.WebApi.Entities;
using System.Collections.Generic;

namespace Inventory.WebApi.Services
{
    public interface IProductCategoryRepository
    {
        void AddProductCategory(ProductCategory productCategory);

        void DeleteProductCategory(ProductCategory productCategory);

        IEnumerable<ProductCategory> GetProductCategories();

        ProductCategory GetProductCategory(int productCategoryId);

        bool ProductCategoryExists(int productCategoryId);

        bool Save();
    }
}