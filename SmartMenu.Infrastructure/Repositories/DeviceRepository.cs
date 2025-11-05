using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device?> GetByIdAsync(long id);
        Task AddAsync(Device device);
        Task UpdateAsync(Device device);
        Task DeleteAsync(long id);
    }

    public class DeviceRepository : IDeviceRepository
    {
        private readonly SmartMenuDbContext _context;
        public DeviceRepository(SmartMenuDbContext context) => _context = context;

        public async Task<IEnumerable<Device>> GetAllAsync() =>
            await _context.Devices.Include(d => d.Store).AsNoTracking().ToListAsync();

        public async Task<Device?> GetByIdAsync(long id) =>
            await _context.Devices.Include(d => d.Store)
                                  .FirstOrDefaultAsync(d => d.DeviceId == id);

        public async Task AddAsync(Device device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Device device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }
    }
}
