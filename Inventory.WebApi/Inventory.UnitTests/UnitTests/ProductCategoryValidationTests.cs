using FluentAssertions;
using GenFu;
using Inventory.Tests.ClassFixtures;
using Inventory.WebApi.Controllers;
using Inventory.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Inventory.Tests.UnitTests
{
    public class ProductCategoryValidationTests : IClassFixture<ProductCategoryFixture>, IClassFixture<AutoMapperFixture>
    {
        [Fact]
        public void When_adding_a_product_category_as_a_null_value_it_should_return_a_bad_request_code()
        {
            // Arrange
            var controller = new ProductCategoriesController(null, null);

            // Act
            var result = controller.Add(null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void When_adding_a_product_category_the_model_state_should_be_validated()
        {
            // Arrange
            var controller = new ProductCategoriesController(null, null);

            // Act
            controller.ModelState.AddModelError("", "Error");
            var result = controller.Add(new ProductCategoryForPostDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("123456789012345678901234567890123456789012345678901")]
        public void When_validating_the_state_of_a_product_category_with_an_invalid_name_it_should_return_an_error(string name)
        {
            // Arrange
            var model = new ProductCategoryForPostDto();
            model.Name = name;
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeFalse();
            result.Should().HaveCount(1);
            var failure = result.First();
            failure.MemberNames.Should().ContainSingle("Name");
        }

        [Fact]
        public void When_validating_the_state_of_a_product_category_with_valid_data_it_should_pass_the_validations()
        {
            // Arrange
            var model = A.New<ProductCategoryForPostDto>();
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeTrue();
        }

        [Fact]
        public void When_updating_a_product_category_as_a_null_value_it_should_return_a_bad_request_code()
        {
            // Arrange
            var controller = new ProductCategoriesController(null, null);

            // Act
            var result = controller.Update(0, null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void When_updating_a_product_category_the_model_state_should_be_validated()
        {
            // Arrange
            var controller = new ProductCategoriesController(null, null);

            // Act
            controller.ModelState.AddModelError("", "Error");
            var result = controller.Update(0, new ProductCategoryForPostDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}