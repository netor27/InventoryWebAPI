using GenFu;
using Inventory.WebApi.Models;
using System;

namespace Inventory.Tests.ClassFixtures
{
    public class ProductCategoryFixture : IDisposable
    {
        public ProductCategoryFixture()
        {
            // Configure Genfu
            GenFu.GenFu.Configure<ProductCategoryForPostDto>()
                .Fill(p => p.Name).AsMusicGenreName();
        }

        public void Dispose()
        {
        }
    }
}