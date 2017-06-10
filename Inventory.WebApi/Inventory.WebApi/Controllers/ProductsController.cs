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
    public class ProductsController : Controller
    {
        private ILogger<ProductsController> _logger;
        private IProductRepository _productRepository;

        public ProductsController(ILogger<ProductsController> logger,
            IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add([FromBody] ProductForPostDto productDto)
        {
            try
            {
                if (productDto == null)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var product = Mapper.Map<Product>(productDto);
                _productRepository.AddProduct(product);

                if (!_productRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                var productDtoResult = Mapper.Map<ProductDto>(product);
                return CreatedAtRoute("GetProduct", new { id = productDtoResult.Id }, productDtoResult);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while adding a new product.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var product = _productRepository.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }

                _productRepository.DeleteProduct(product);
                if (!_productRepository.Save())
                {
                    return StatusCode(500, "A problem happened while handling your request.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while deleting the product with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                var entities = _productRepository.GetProducts();
                var results = Mapper.Map<IEnumerable<ProductDto>>(entities);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting products.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _productRepository.GetProduct(id);
                if (product == null)
                {
                    return NotFound();
                }

                var result = Mapper.Map<ProductDto>(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting product with id {id}.", ex);
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductForPostDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productEntity = _productRepository.GetProduct(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(productDto, productEntity);

            if (!_productRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}