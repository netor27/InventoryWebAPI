using Inventory.WebApi.Entities;
using Inventory.WebApi.Models;
using System;

namespace Inventory.Tests.ClassFixtures
{
    public class AutoMapperFixture : IDisposable
    {
        public AutoMapperFixture()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductCategory, ProductCategoryDto>();
                cfg.CreateMap<ProductDto, Product>();
                cfg.CreateMap<ProductCategoryDto, ProductCategory>();
                cfg.CreateMap<ProductForPostDto, Product>();
                cfg.CreateMap<ProductCategoryForPostDto, ProductCategory>();
            });
        }

        public void Dispose()
        {
        }
    }
}
