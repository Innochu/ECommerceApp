using ECommerceApp.Domain.Entities;

namespace ECommerceApp.Application.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
    }
}
