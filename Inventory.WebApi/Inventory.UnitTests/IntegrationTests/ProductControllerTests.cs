using FluentAssertions;
using GenFu;
using Inventory.Tests.ClassFixtures;
using Inventory.Tests.HelperClasses;
using Inventory.UnitTests.Stubs;
using Inventory.WebApi.Controllers;
using Inventory.WebApi.Models;
using Inventory.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Inventory.UnitTests.IntegrationTests
{
    public class ProductControllerTests : IClassFixture<ProductFixture>, IClassFixture<ProductRepositoryFixture>, IClassFixture<AutoMapperFixture>, IClassFixture<ProductCategoryRepositoryFixture>
    {
        private ILogger<ProductsController> _logger;
        private ILogger<ProductCategoriesController> _categoryLogger;

        private IProductRepository _repository;

        private IProductCategoryRepository _productCategoryRepository;

        public ProductControllerTests(ProductFixture productFixture, ProductRepositoryFixture productRepositoryFixture, ProductCategoryRepositoryFixture productCategoryRepositoryFixture)
        {
            _repository = productRepositoryFixture.Repository;
            _productCategoryRepository = productCategoryRepositoryFixture.Repository;
            _logger = new StubLogger<ProductsController>();
            _categoryLogger = new StubLogger<ProductCategoriesController>();
        }

        [Fact]
        public void When_adding_a_new_valid_product_the_new_element_should_be_returned_with_valid_values()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);
            var categoryController = new ProductCategoriesController(_categoryLogger, _productCategoryRepository);
            var category = ValidDataHelper.GetValidProductCategoryDto(categoryController);
            var product = A.New<ProductForPostDto>();
            product.ProductCategoryId = category.Id;

            // Act
            var result = controller.Add(product);
            var retrievedElement = result.ValidateResponseAndCastTo<ProductDto, CreatedAtRouteResult>
                ((int)HttpStatusCodes.Created);

            retrievedElement.Name.Should().Be(product.Name);
            retrievedElement.Id.Should().NotBe(0);
        }
        
        [Fact]
        public void When_deleting_a_product_the_query_for_that_id_should_return_a_not_found_code()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);
            var originalElement = ValidDataHelper.GetValidProductDto(controller);

            // Act
            var result = controller.Delete(originalElement.Id);
            var getResult = controller.GetProduct(originalElement.Id);

            // Asset
            result.Should().BeOfType<NoContentResult>();
            getResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_deleting_an_invalid_product_a_not_found_code_should_be_returned()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);

            // Act
            var result = controller.Delete(-1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_getting_a_product_with_a_valid_id_it_should_return_a_valid_element()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);
            var originalElement = ValidDataHelper.GetValidProductDto(controller);

            // Act
            var result = controller.GetProduct(originalElement.Id);
            var retrievedElement = result.ValidateResponseAndCastTo<ProductDto, OkObjectResult>((int)HttpStatusCodes.Ok);

            // Assert
            retrievedElement.ShouldBeEquivalentTo(originalElement);
        }

        [Fact]
        public void When_getting_a_product_with_an_invalid_id_it_should_return_a_not_found_code()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);

            // Act
            var result = controller.GetProduct(-1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_getting_all_products_it_should_return_a_list_with_elements()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);

            // Act
            var result = controller.GetProducts();
            var listResult = result.ValidateResponseAndCastTo<List<ProductDto>, OkObjectResult>((int)HttpStatusCodes.Ok);

            // Assert
            listResult.Any().Should().BeTrue();
        }

        [Fact]
        public void When_updating_an_invalid_product_a_not_found_code_should_be_returned()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);

            // Act
            var result = controller.Update(-1, new ProductForPostDto());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_updating_a_product_the_query_for_that_id_should_return_the_updated_element()
        {
            // Arrange
            var controller = new ProductsController(_logger, _repository);
            var originalElement = ValidDataHelper.GetValidProductDto(controller);
            var categoryController = new ProductCategoriesController(_categoryLogger, _productCategoryRepository);
            var category = ValidDataHelper.GetValidProductCategoryDto(categoryController, false);
            var updatedValue = A.New<ProductForPostDto>();
            updatedValue.ProductCategoryId = category.Id;
            AutoMapper.Mapper.Map(updatedValue, originalElement);

            // Act
            var result = controller.Update(originalElement.Id, updatedValue);
            var getResult = controller.GetProduct(originalElement.Id);
            var retrievedValue = getResult.ValidateResponseAndCastTo<ProductDto, OkObjectResult>
                ((int)HttpStatusCodes.Ok);

            // Asset
            result.Should().BeOfType<NoContentResult>();
            retrievedValue.ShouldBeEquivalentTo(originalElement);            
        }
    }
}