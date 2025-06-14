using ECommerceApp.Application.Service.Interface;
using ECommerceApp.Domain.Dto;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.ApiService.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponseModel>> GetCart()
        {
            var cartItems = await _cartService.GetCartAsync();
            return Ok(new BaseResponseModel
            {
                success = true,
                Data = cartItems
            });
        }

  
        [HttpPost]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] AddProductToCartDto item)
        {
            if (item == null) return BadRequest();
            await _cartService.AddToCartAsync(item);
            return CreatedAtAction(nameof(GetCart), new { id = item.ProductId }, item);
        }

        // Update an item in the cart
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCartItem(int id, [FromBody] CartItem item)
        {
            if (id != item.Id) return BadRequest();
            var updated = await _cartService.UpdateCartItemAsync(item);
            if (!updated) return NotFound();
            return NoContent();
        }

        // Remove an item from the cart
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var deleted = await _cartService.RemoveFromCartAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
