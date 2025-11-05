using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(long id);
        Task<Device> CreateAsync(Device device);
        Task<Device?> UpdateAsync(Device device);
        Task<bool> DeleteAsync(long id);
    }

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repo;
        public DeviceService(IDeviceRepository repo) => _repo = repo;

        public async Task<IEnumerable<Device>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Device?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);
        public async Task<Device> CreateAsync(Device device)
        {
            await _repo.AddAsync(device);
            return device;
        }
        public async Task<Device?> UpdateAsync(Device device)
        {
            await _repo.UpdateAsync(device);
            return device;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
