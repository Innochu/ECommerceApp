using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Repository.Interface
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartAsync();
        Task<bool> AddToCartAsync(CartItem item);
        Task<bool> UpdateCartItemAsync(CartItem item);
        Task<bool> RemoveFromCartAsync(int id);
    }
}
