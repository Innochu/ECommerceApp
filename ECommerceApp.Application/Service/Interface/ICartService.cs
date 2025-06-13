using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Service.Interface
{
    public interface ICartService
    {
        Task<CartItem> GetCartAsync();
        Task<bool> AddToCartAsync(CartItem item);
        Task<bool> UpdateCartItemAsync(CartItem item);
        Task<bool> RemoveFromCartAsync(int id);
    }
}
