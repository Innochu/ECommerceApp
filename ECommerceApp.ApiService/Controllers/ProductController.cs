using ECommerceApp.Application.Service.Interface;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.API.Controllers
{
    /// <summary>
    /// Handles product-related operations such as retrieving and deleting products.
    /// </summary>
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productService">The service for managing products.</param>
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products wrapped in a <see cref="BaseResponseModel"/>.</returns>
        /// <response code="200">Returns the list of products.</response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<ActionResult<BaseResponseModel>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(new BaseResponseModel
            {
                success = true,
                Data = products
            });
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A <see cref="BaseResponseModel"/> indicating success or failure.</returns>
        /// <response code="200">Returns success or an error message if not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return Ok(new BaseResponseModel { success = false, ErrorMessage = "Not Found" });
            }
            return Ok(new BaseResponseModel { success = true });
        }
    }
}
