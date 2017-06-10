using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Tests.HelperClasses
{
    public static class OkObjectResultCaster
    {
        public static T ValidateResponseAndCastTo<T, ResultType>(this IActionResult actionResult, int expectedStatusCode) 
            where ResultType : ObjectResult
        {
            actionResult.Should().BeOfType<ResultType>();
            var okResult = actionResult as ObjectResult;
            okResult.StatusCode.Should().Be(expectedStatusCode);
            okResult.Value.Should().BeOfType<T>();
            return (T)okResult.Value;
        }
    }
}