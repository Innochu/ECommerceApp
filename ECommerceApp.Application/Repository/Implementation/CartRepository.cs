using ECommerceApp.Application.Repository.Interface;
using ECommerceApp.Domain.Entities;
using ECommerceApp.Infrastructure.DbContextFolder;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Application.Repository.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> GetCartAsync()
        {
            return await _context.CartItems.Include(c => c.Product).FirstOrDefaultAsync();
        }

        public async Task<bool> AddToCartAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCartItemAsync(CartItem item)
        {
            _context.CartItems.Update(item);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveFromCartAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return false;

            _context.CartItems.Remove(item);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
