using Microsoft.AspNetCore.Mvc;
using Inventory.WebApi.Entities;
using Microsoft.Extensions.Logging;
using Inventory.WebApi.Services;
using System;
using Inventory.WebApi.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Inventory.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductCategoriesController : Controller
    {
        private ILogger<ProductCategoriesController> _logger;
        private IProductCategoryRepository _productCategoryRepository;

        public ProductCategoriesController(ILogger<ProductCategoriesController> logger,
            IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        
        [HttpGet]
        public IActionResult GetProductCategories()
        {
            try
            {
                var entities = _productCategoryRepository.GetProductCategories();
                var results = Mapper.Map<IEnumerable<ProductCategoryDto>>(entities);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting product categories.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductCategory(int id)
        {
            try
            {
                var productCategory = _productCategoryRepository.GetProductCategory(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                var result = Mapper.Map<ProductCategoryDto>(productCategory);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting product category with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}
