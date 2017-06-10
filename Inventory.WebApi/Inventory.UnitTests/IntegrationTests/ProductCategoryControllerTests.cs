using AutoMapper;
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
    public class ProductCategoryControllerTests : IClassFixture<ProductCategoryFixture>
    {
        private static ILogger<ProductCategoriesController> _logger;

        private IProductCategoryRepository _repository;

        public ProductCategoryControllerTests(ProductCategoryFixture productCategoryFixture)
        {
            _repository = productCategoryFixture.Repository;
            _logger = new StubLogger<ProductCategoriesController>();
        }

        [Fact]
        public void When_adding_a_new_valid_product_category_the_new_element_should_be_returned_with_valid_values()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var productCategory = A.New<ProductCategoryForPostDto>();
            var result = controller.Add(productCategory);
            var retrievedElement = result.ValidateResponseAndCastTo<ProductCategoryDto, CreatedAtRouteResult>
                ((int)HttpStatusCodes.Created);

            retrievedElement.Name.Should().Be(productCategory.Name);
            retrievedElement.Id.Should().NotBe(0);
        }
        
        [Fact]
        public void When_deleting_a_product_category_the_query_for_that_id_should_return_a_not_found_code()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);
            var originalElement = GetValidProductCategoryDto(controller);

            // Act
            var result = controller.Delete(originalElement.Id);
            var getResult = controller.GetProductCategory(originalElement.Id);

            // Asset
            result.Should().BeOfType<NoContentResult>();
            getResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_deleting_an_invalid_product_category_a_not_found_code_should_be_returned()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.Delete(-1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_getting_a_product_category_with_a_valid_id_it_should_return_a_valid_element()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);
            var originalElement = GetValidProductCategoryDto(controller);

            // Act
            var result = controller.GetProductCategory(originalElement.Id);
            var retrievedElement = result.ValidateResponseAndCastTo<ProductCategoryDto, OkObjectResult>((int)HttpStatusCodes.Ok);

            // Assert
            retrievedElement.ShouldBeEquivalentTo(originalElement);
        }

        [Fact]
        public void When_getting_a_product_category_with_an_invalid_id_it_should_return_a_not_found_code()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.GetProductCategory(-1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_getting_all_product_categories_it_should_return_a_list_with_elements()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.GetProductCategories();
            var listResult = result.ValidateResponseAndCastTo<List<ProductCategoryDto>, OkObjectResult>((int)HttpStatusCodes.Ok);

            // Assert
            listResult.Any().Should().BeTrue();
        }

        [Fact]
        public void When_updating_an_invalid_product_category_a_not_found_code_should_be_returned()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);

            // Act
            var result = controller.Update(-1, new ProductCategoryForPostDto());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void When_updating_a_product_category_the_query_for_that_id_should_return_the_updated_element()
        {
            // Arrange
            var controller = new ProductCategoriesController(_logger, _repository);
            var originalElement = GetValidProductCategoryDto(controller);
            originalElement.Name = $"UpdatedValue for Id={originalElement.Id}";
            var updatedValue = new ProductCategoryForPostDto() { Name = originalElement.Name };
            
            // Act
            var result = controller.Update(originalElement.Id, updatedValue);
            var getResult = controller.GetProductCategory(originalElement.Id);
            var retrievedValue = getResult.ValidateResponseAndCastTo<ProductCategoryDto, OkObjectResult>
                ((int)HttpStatusCodes.Ok);

            // Asset
            result.Should().BeOfType<NoContentResult>();
            retrievedValue.ShouldBeEquivalentTo(originalElement);            
        }

        private ProductCategoryDto GetValidProductCategoryDto(ProductCategoriesController controller)
        {
            var result = controller.GetProductCategories();
            var listResult = result.ValidateResponseAndCastTo<List<ProductCategoryDto>, OkObjectResult>((int)HttpStatusCodes.Ok);
            listResult.Any().Should().BeTrue();
            return listResult.First();
        }
    }
}