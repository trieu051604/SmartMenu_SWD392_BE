using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(long id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteAsync(long id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;

        public CategoryService(ICategoryRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Category?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);

        public async Task<Category> CreateAsync(Category category)
        {
            await _repo.AddAsync(category);
            return category;
        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            await _repo.UpdateAsync(category);
            return category;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
