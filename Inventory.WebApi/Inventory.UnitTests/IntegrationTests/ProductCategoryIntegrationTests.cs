using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory.WebApi.Services;
using Inventory.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Inventory.UnitTests.Stubs;
using Microsoft.Extensions.DependencyInjection;
using Inventory.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Inventory.WebApi.Models;
using System.Linq;

namespace Inventory.UnitTests.IntegrationTests
{
    [TestClass]
    public class ProductCategoryIntegrationTests
    {
        private static IProductCategoryRepository _repository;
        private static ILogger<ProductCategoriesController> _logger;
        private static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=InventoryInfoDB;Trusted_Connection=True;";
                
        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkSqlServer()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<ProductInfoContext>();                        
            builder.UseSqlServer(connectionString).UseInternalServiceProvider(serviceProvider);
            var context = new ProductInfoContext(builder.Options);
            context.Database.Migrate();

            _repository = new ProductCategoryRepository(context);
            _logger = new StubLogger<ProductCategoriesController>();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<WebApi.Entities.Product, WebApi.Models.ProductDto>();
                cfg.CreateMap<WebApi.Entities.ProductCategory, WebApi.Models.ProductCategoryDto>();
                cfg.CreateMap<WebApi.Models.ProductDto, WebApi.Entities.Product>();
                cfg.CreateMap<WebApi.Models.ProductCategoryDto, WebApi.Entities.ProductCategory>();
                cfg.CreateMap<WebApi.Models.ProductCategoryForPostDto, WebApi.Entities.ProductCategory>();
            });
        }

        [TestMethod]
        public void Get_product_categories_should_return_a_list_with_elements()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.GetProductCategories();

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsTrue(okResult.StatusCode == 200);

            Assert.IsInstanceOfType(okResult.Value, typeof(List<ProductCategoryDto>));
            var listResult = okResult.Value as List<ProductCategoryDto>;
            Assert.IsTrue(listResult.Any());
        }
    }
}
