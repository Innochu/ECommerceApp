using ECommerceApp.Application.Service.Interface;
using ECommerceApp.Application.Services;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<BaseResponseModel>> GetAllProducts()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(new BaseResponseModel
        {
            success = true,
            Data = products
        });
    }

    [HttpDelete("{id}")]
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
