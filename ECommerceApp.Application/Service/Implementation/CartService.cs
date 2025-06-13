using ECommerceApp.Application.Repository.Interface;
using ECommerceApp.Application.Service.Interface;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartItem> GetCartAsync()
        {
            return await _cartRepository.GetCartAsync();
        }

        public async Task<bool> AddToCartAsync(CartItem item)
        {
            return await _cartRepository.AddToCartAsync(item);
        }

        public async Task<bool> UpdateCartItemAsync(CartItem item)
        {
            return await _cartRepository.UpdateCartItemAsync(item);
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            return await _cartRepository.RemoveFromCartAsync(id);
        }
    }
}
