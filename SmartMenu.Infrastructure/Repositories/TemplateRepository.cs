using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<MenuTemplate>> GetAllAsync();
        Task<MenuTemplate?> GetByIdAsync(long id);
        Task AddAsync(MenuTemplate template);
        Task UpdateAsync(MenuTemplate template);
        Task DeleteAsync(long id);
    }

    public class TemplateRepository : ITemplateRepository
    {
        private readonly SmartMenuDbContext _context;

        public TemplateRepository(SmartMenuDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MenuTemplate>> GetAllAsync() =>
            await _context.MenuTemplates.AsNoTracking().ToListAsync();

        public async Task<MenuTemplate?> GetByIdAsync(long id) =>
            await _context.MenuTemplates.FindAsync(id);

        public async Task AddAsync(MenuTemplate template)
        {
            await _context.MenuTemplates.AddAsync(template);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MenuTemplate template)
        {
            _context.MenuTemplates.Update(template);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var template = await _context.MenuTemplates.FindAsync(id);
            if (template != null)
            {
                _context.MenuTemplates.Remove(template);
                await _context.SaveChangesAsync();
            }
        }
    }
}
