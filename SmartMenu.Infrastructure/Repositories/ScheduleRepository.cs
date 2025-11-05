using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface IScheduleRepository
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(long id);
        Task AddAsync(Schedule schedule);
        Task UpdateAsync(Schedule schedule);
        Task DeleteAsync(long id);
    }

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly SmartMenuDbContext _context;
        public ScheduleRepository(SmartMenuDbContext context) => _context = context;

        public async Task<IEnumerable<Schedule>> GetAllAsync() =>
            await _context.Schedules
                .Include(s => s.Playlist)
                .Include(s => s.Device)
                .AsNoTracking()
                .ToListAsync();

        public async Task<Schedule?> GetByIdAsync(long id) =>
            await _context.Schedules
                .Include(s => s.Playlist)
                .Include(s => s.Device)
                .FirstOrDefaultAsync(s => s.ScheduleId == id);

        public async Task AddAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
