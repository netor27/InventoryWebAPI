using Inventory.WebApi.Entities;
using System.Collections.Generic;

namespace Inventory.WebApi.Services
{
    public interface IProductRepository
    {
        void AddProduct(Product product);

        void DeleteProduct(Product product);

        Product GetProduct(int productId);

        IEnumerable<Product> GetProducts();

        bool ProductExists(int productId);

        bool Save();
    }
}