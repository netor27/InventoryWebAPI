using GenFu;
using Inventory.Tests.HelperClasses;
using Inventory.WebApi.Entities;
using Inventory.WebApi.Extensions;
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

            // Configure autoMapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductCategory, ProductCategoryDto>();
                cfg.CreateMap<ProductCategoryDto, ProductCategory>();
                cfg.CreateMap<ProductCategoryForPostDto, ProductCategory>();
            });

            // Configure Genfu
            GenFu.GenFu.Configure<ProductCategoryForPostDto>()
                .Fill(p => p.Name).AsMusicGenreName();
        }

        public IProductCategoryRepository Repository { get; private set; }

        public void Dispose()
        {
            Repository = null;
        }
    }
}