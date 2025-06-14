using ECommerceApp.Application.Repository.Interface;


namespace ECommerceApp.Application.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<CartItem>> GetCartAsync()
        {
            return await _cartRepository.GetCartAsync();
        }

        public async Task<bool> AddToCartAsync(AddProductToCartDto item)
        {
            var cartItem = new CartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
            };

            return await _cartRepository.AddToCartAsync(cartItem);
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
