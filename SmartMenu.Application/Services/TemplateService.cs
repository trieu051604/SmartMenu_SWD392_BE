using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface ITemplateService
    {
        Task<IEnumerable<MenuTemplate>> GetAllAsync();
        Task<MenuTemplate?> GetByIdAsync(long id);
        Task<MenuTemplate> CreateAsync(MenuTemplate template);
        Task<MenuTemplate?> UpdateAsync(MenuTemplate template);
        Task<bool> DeleteAsync(long id);
    }

    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _repo;

        public TemplateService(ITemplateRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MenuTemplate>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<MenuTemplate?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);

        public async Task<MenuTemplate> CreateAsync(MenuTemplate template)
        {
            await _repo.AddAsync(template);
            return template;
        }

        public async Task<MenuTemplate?> UpdateAsync(MenuTemplate template)
        {
            await _repo.UpdateAsync(template);
            return template;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
