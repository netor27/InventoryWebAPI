using AutoMapper;
using Inventory.WebApi.Entities;
using Inventory.WebApi.Models;
using Inventory.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductCategoryForPostDto productCategoryDto)
        {
            try
            {
                if (productCategoryDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var productCategory = Mapper.Map<ProductCategory>(productCategoryDto);
                _productCategoryRepository.AddProductCategory(productCategory);

                if (!_productCategoryRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                var productCategoryDtoResult = Mapper.Map<ProductCategoryDto>(productCategory);
                return CreatedAtRoute("GetProductCategory", new { id = productCategoryDtoResult.Id }, productCategoryDtoResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding a new product category.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var productCategory = _productCategoryRepository.GetProductCategory(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                _productCategoryRepository.DeleteProductCategory(productCategory);
                if (!_productCategoryRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting the product category with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
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

        [HttpGet("{id}", Name = "GetProductCategory")]
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductCategoryForPostDto productCategoryDto)
        {
            if (productCategoryDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productCategoryEntity = _productCategoryRepository.GetProductCategory(id);
            if (productCategoryEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(productCategoryDto, productCategoryEntity);

            if (!_productCategoryRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}