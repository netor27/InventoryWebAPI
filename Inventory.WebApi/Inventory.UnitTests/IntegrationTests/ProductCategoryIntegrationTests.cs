using FluentAssertions;
using Inventory.Tests.ClassFixtures;
using Inventory.Tests.HelperClasses;
using Inventory.UnitTests.Stubs;
using Inventory.WebApi.Controllers;
using Inventory.WebApi.Models;
using Inventory.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Inventory.UnitTests.IntegrationTests
{
    [TestClass]
    public class ProductCategoryIntegrationTests : IClassFixture<ProductCategoryFixture>
    {
        private static ILogger<ProductCategoriesController> _logger;

        private IProductCategoryRepository _repository;

        public ProductCategoryIntegrationTests(ProductCategoryFixture productCategoryFixture)
        {
            _repository = productCategoryFixture.Repository;
            _logger = new StubLogger<ProductCategoriesController>();
        }

        [Fact]
        public void Get_product_categories_should_return_a_list_with_elements()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.GetProductCategories();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be((int)HttpStatusCodes.Ok);
            okResult.Value.Should().BeOfType<List<ProductCategoryDto>>();
            var listResult = okResult.Value as List<ProductCategoryDto>;
            listResult.Any().Should().BeTrue();
        }
    }
}