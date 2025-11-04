using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(long id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(long id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Product?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);
        public async Task<Product> CreateAsync(Product product)
        {
            await _repo.AddAsync(product);
            return product;
        }
        public async Task<Product?> UpdateAsync(Product product)
        {
            await _repo.UpdateAsync(product);
            return product;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
