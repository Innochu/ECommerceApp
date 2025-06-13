using ECommerceApp.Application.Service.Interface;
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

        // Get all cart items
        [HttpGet]
        public async Task<ActionResult<BaseResponseModel>> GetCart()
        {
            return Ok(await _cartService.GetCartAsync());
        }

        // Add an item to the cart
        [HttpPost]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] CartItem item)
        {
            if (item == null) return BadRequest();
            await _cartService.AddToCartAsync(item);
            return CreatedAtAction(nameof(GetCart), new { id = item.Id }, item);
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
