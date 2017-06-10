using Inventory.WebApi.Entities;
using Inventory.WebApi.Models;
using System;
using System.Collections.Generic;

namespace Inventory.Tests.ClassFixtures
{
    public class ProductFixture : IDisposable
    {
        public ProductFixture()
        {
            // Configure Genfu
            var validPrices = new List<double>();
            var r = new Random();
            for (int i = 1; i <= 100; i++)
            {
                double value = 50 + (i * 50 * r.NextDouble());
                validPrices.Add(value);
            }

            GenFu.GenFu.Configure<ProductForPostDto>()
                .Fill(p => p.Price).WithRandom(validPrices)
                .Fill(p => p.StockAmount).WithinRange(10, 500);
        }

        public void Dispose()
        {
        }
    }
}