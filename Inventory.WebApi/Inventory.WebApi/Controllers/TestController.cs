using Inventory.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private ProductInfoContext _context;

        public TestController(ProductInfoContext ctx)
        {
            _context = ctx;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}