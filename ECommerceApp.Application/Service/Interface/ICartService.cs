using ECommerceApp.Domain.Dto;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Service.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<CartItem>> GetCartAsync();
        Task<bool> AddToCartAsync(AddProductToCartDto item);
        Task<bool> UpdateCartItemAsync(CartItem item);
        Task<bool> RemoveFromCartAsync(int id);
    }
}
