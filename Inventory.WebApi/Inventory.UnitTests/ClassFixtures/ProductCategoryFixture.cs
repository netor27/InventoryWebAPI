using Inventory.Tests.HelperClasses;
using Inventory.WebApi.Entities;
using Inventory.WebApi.Models;
using Inventory.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Inventory.Tests.ClassFixtures
{
    public class ProductCategoryFixture : IDisposable
    {
        public ProductCategoryFixture()
        {
            var serviceProvider = new ServiceCollection()
               .AddEntityFrameworkSqlServer()
               .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ProductInfoContext>();
            builder.UseSqlServer(ConstantValues.connectionString).UseInternalServiceProvider(serviceProvider);

            var context = new ProductInfoContext(builder.Options);
            context.Database.Migrate();

            Repository = new ProductCategoryRepository(context);

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductCategory, ProductCategoryDto>();
                cfg.CreateMap<ProductCategoryDto, ProductCategory>();
                cfg.CreateMap<ProductCategoryForPostDto, ProductCategory>();
            });
        }

        public IProductCategoryRepository Repository { get; private set; }

        public void Dispose()
        {
            Repository = null;
        }
    }
}