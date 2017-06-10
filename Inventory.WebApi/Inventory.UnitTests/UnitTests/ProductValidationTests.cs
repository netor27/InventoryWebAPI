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
    public class ProductValidationTests : IClassFixture<ProductFixture>, IClassFixture<AutoMapperFixture>
    {
        [Fact]
        public void When_adding_a_product_as_a_null_value_it_should_return_a_bad_request_code()
        {
            // Arrange
            var controller = new ProductsController(null, null);

            // Act
            var result = controller.Add(null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void When_adding_a_product__the_model_state_should_be_validated()
        {
            // Arrange
            var controller = new ProductsController(null, null);

            // Act
            controller.ModelState.AddModelError("", "Error");
            var result = controller.Add(new ProductForPostDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("123456789012345678901234567890123456789012345678901")]
        public void When_validating_the_state_of_a_product_with_an_invalid_name_it_should_return_an_error(string name)
        {
            // Arrange
            var model = A.New<ProductForPostDto>();
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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("123456789012345678901234567890123456789012345678901123456789012345678901234567890123456789012345678901123456789012345678901234567890123456789012345678901123456789012345678901234567890123456789012345678901123456789012345678901234567890123456789012345678901")]
        public void When_validating_the_state_of_a_product_with_an_invalid_image_it_should_return_an_error(string image)
        {
            // Arrange
            var model = A.New<ProductForPostDto>();
            model.Image = image;
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeFalse();
            result.Should().HaveCount(1);
            var failure = result.First();
            failure.MemberNames.Should().ContainSingle("Image");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(-1)]
        [InlineData(0)]
        public void When_validating_the_state_of_a_product_with_an_invalid_price_it_should_return_an_error(double price)
        {
            // Arrange
            var model = A.New<ProductForPostDto>();
            model.Price = price;
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeFalse();
            result.Should().HaveCount(1);
            var failure = result.First();
            failure.MemberNames.Should().ContainSingle("Price");
        }

        [Theory]
        [InlineData(null)]
        [InlineData(-1)]
        [InlineData(0)]
        public void When_validating_the_state_of_a_product_with_an_invalid_stock_amount_it_should_return_an_error(int stockAmount)
        {
            // Arrange
            var model = A.New<ProductForPostDto>();
            model.StockAmount = stockAmount;
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeFalse();
            result.Should().HaveCount(1);
            var failure = result.First();
            failure.MemberNames.Should().ContainSingle("Price");
        }

        [Fact]
        public void When_validating_the_state_of_a_product_with_valid_data_it_should_pass_the_validations()
        {
            // Arrange
            var model = A.New<ProductForPostDto>();
            var context = new ValidationContext(model, null, null);
            var result = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(model, context, result, true);

            // Assert
            valid.Should().BeTrue();
        }

        [Fact]
        public void When_updating_a_product_as_a_null_value_it_should_return_a_bad_request_code()
        {
            // Arrange
            var controller = new ProductsController(null, null);

            // Act
            var result = controller.Update(0, null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void When_updating_a_product_the_model_state_should_be_validated()
        {
            // Arrange
            var controller = new ProductsController(null, null);

            // Act
            controller.ModelState.AddModelError("", "Error");
            var result = controller.Update(0, new ProductForPostDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}