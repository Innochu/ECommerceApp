using ECommerceApp.Application.Service.Interface;
using ECommerceApp.Domain.Dto;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.ApiService.Controllers
{
    /// <summary>
    /// Manages shopping cart operations such as retrieving, adding, updating, and removing items.
    /// </summary>
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartController"/> class.
        /// </summary>
        /// <param name="cartService">Service to manage cart operations.</param>
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retrieves the current user's cart items.
        /// </summary>
        /// <returns>A list of cart items wrapped in a <see cref="BaseResponseModel"/>.</returns>
        /// <response code="200">Returns the current cart contents.</response>
        [HttpGet]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        public async Task<ActionResult<BaseResponseModel>> GetCart()
        {
            var cartItems = await _cartService.GetCartAsync();
            return Ok(new BaseResponseModel
            {
                success = true,
                Data = cartItems
            });
        }

        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="item">The product details to add to the cart.</param>
        /// <returns>The added cart item with a reference to the <see cref="GetCart"/> endpoint.</returns>
        /// <response code="201">Product added successfully.</response>
        /// <response code="400">Invalid input data.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CartItem), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CartItem>> AddToCart([FromBody] AddProductToCartDto item)
        {
            if (item == null) return BadRequest();
            await _cartService.AddToCartAsync(item);
            return CreatedAtAction(nameof(GetCart), new { id = item.ProductId }, item);
        }

        /// <summary>
        /// Updates an existing item in the cart.
        /// </summary>
        /// <param name="id">The ID of the cart item to update.</param>
        /// <param name="item">The updated cart item data.</param>
        /// <returns>No content on success, or error status.</returns>
        /// <response code="204">Cart item updated successfully.</response>
        /// <response code="400">Mismatch between route ID and item ID.</response>
        /// <response code="404">Item not found in the cart.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> UpdateCartItem(int id, [FromBody] CartItem item)
        {
            if (id != item.Id) return BadRequest();
            var updated = await _cartService.UpdateCartItemAsync(item);
            if (!updated) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Removes a product from the cart.
        /// </summary>
        /// <param name="id">The ID of the cart item to remove.</param>
        /// <returns>No content on success, or not found.</returns>
        /// <response code="204">Item removed successfully.</response>
        /// <response code="404">Item not found in the cart.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> RemoveFromCart(int id)
        {
            var deleted = await _cartService.RemoveFromCartAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
