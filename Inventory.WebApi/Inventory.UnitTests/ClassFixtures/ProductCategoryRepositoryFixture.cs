using Inventory.Tests.HelperClasses;
using Inventory.WebApi.Entities;
using Inventory.WebApi.Extensions;
using Inventory.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Inventory.Tests.ClassFixtures
{
    public class ProductCategoryRepositoryFixture : IDisposable
    {
        public ProductCategoryRepositoryFixture()
        {
            // Configure our repository
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkSqlServer()
               .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ProductInfoContext>();
            builder.UseSqlServer(ConstantValues.connectionString).UseInternalServiceProvider(serviceProvider);

            var context = new ProductInfoContext(builder.Options);

            // validate we have the last version of the database and at least the minimum set of data
            context.Database.Migrate();
            context.EnsureSeedDataForContext();

            Repository = new ProductCategoryRepository(context);
        }

        public IProductCategoryRepository Repository { get; private set; }

        public void Dispose()
        {
            Repository = null;
        }
    }
}