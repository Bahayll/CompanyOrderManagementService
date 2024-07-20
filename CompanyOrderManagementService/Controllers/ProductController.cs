using CompanyOrderManagementService.DTO.CompanyDTO;
using CompanyOrderManagementService.DTO.CompanyPostDTO;
using CompanyOrderManagementService.DTO.OrderDTO;
using CompanyOrderManagementService.DTO.ProductDTO;
using CompanyOrderManagementService.Interfaces;
using CompanyOrderManagementService.Mappers;
using CompanyOrderManagementService.Models;
using CompanyOrderManagementService.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CompanyOrderManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;
        public ProductController(IProductRepository productRepository, ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
        }

        /// <summary>
        /// List Product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Products>))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProductsAsync();
            if (products == null || !products.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products.toProductsDTO());
        }

        /// <summary>
        /// List Product with id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Products))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"Product with ID {id} not found.");
            }
            return Ok(product.toProductDTO());
        }

        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Products))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> AddProduct([FromBody] ProductPostDTO product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Different companies cannot sell the same product by design.
            // Let's check if a product with the same name already exists
            var existingProduct = await _productRepository.GetProductByNameAsync(product.ProductName);
            if (existingProduct != null)
            {
                return Conflict("A product with the same name already exists.");
            }

            var productTOcompany = await _companyRepository.GetCompanyByNameAsync(product.CompanyName);
            if (productTOcompany == null)
            {
                return BadRequest("Company not found.");
            }

            var createdProduct = await _productRepository.AddProductAsync(product.toProductPostDTO());
            if (createdProduct == null)
            {
                return StatusCode(500, "Failed to creat company.");
            }
            return Ok(createdProduct.toProductDTO());
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productPostDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(Products))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound($"product with ID {id} not found.");
            }

            var productTOcompany = await _companyRepository.GetCompanyByNameAsync(productUpdateDTO.CompanyName);
            if (productTOcompany == null)
            {
                return BadRequest("Company not found.");
            }

            // Let's check if the current product is the same as the updated product
            if (product.Stock == productUpdateDTO.Stock &&
            product.Price == productUpdateDTO.Price &&
                product.CompanyName == productUpdateDTO.CompanyName)
            {
                return Ok("Product already up to date.");
            }


            var updatedProduct = productUpdateDTO.toProductUpdateDTO();
            var productDto = await _productRepository.UpdateProductAsync(id, updatedProduct);

            if (productDto == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(productDto.toProductDTO());
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var success = await _productRepository.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound($"Product with ID {id} not found.");
            }

            return NoContent();
        }
    }
}
