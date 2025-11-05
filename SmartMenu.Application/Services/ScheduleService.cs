using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface IScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(long id);
        Task<Schedule> CreateAsync(Schedule schedule);
        Task<Schedule?> UpdateAsync(Schedule schedule);
        Task<bool> DeleteAsync(long id);
    }

    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _repo;
        public ScheduleService(IScheduleRepository repo) => _repo = repo;

        public async Task<IEnumerable<Schedule>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Schedule?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);
        public async Task<Schedule> CreateAsync(Schedule schedule)
        {
            await _repo.AddAsync(schedule);
            return schedule;
        }
        public async Task<Schedule?> UpdateAsync(Schedule schedule)
        {
            await _repo.UpdateAsync(schedule);
            return schedule;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
