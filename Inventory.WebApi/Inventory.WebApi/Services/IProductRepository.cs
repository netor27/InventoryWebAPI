using Inventory.WebApi.Entities;
using System.Collections.Generic;

namespace Inventory.WebApi.Services
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);

        IEnumerable<Product> GetProducts();

        void AddProduct(Product product);

        void DeleteProduct(Product product);

        bool ProductExists(int productId);

        bool Save();

    }
}
