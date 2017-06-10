using FluentAssertions;
using Inventory.WebApi.Controllers;
using Inventory.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Tests.HelperClasses
{
    internal static class ValidDataHelper
    {
        internal static ProductDto GetValidProductDto(ProductsController controller, bool first = true)
        {
            var result = controller.GetProducts();
            var listResult = result.ValidateResponseAndCastTo<List<ProductDto>, OkObjectResult>((int)HttpStatusCodes.Ok);
            listResult.Any().Should().BeTrue();
            return first ? listResult.First() : listResult.Last();
        }

        internal static ProductCategoryDto GetValidProductCategoryDto(ProductCategoriesController controller, bool first = true)
        {
            var result = controller.GetProductCategories();
            var listResult = result.ValidateResponseAndCastTo<List<ProductCategoryDto>, OkObjectResult>((int)HttpStatusCodes.Ok);
            listResult.Any().Should().BeTrue();
            return first ? listResult.First() : listResult.Last();
        }
    }
}
