using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<AuditLog?> GetByIdAsync(long id);
        Task AddAsync(AuditLog log);
        Task DeleteAsync(long id);
    }

    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly SmartMenuDbContext _context;
        public AuditLogRepository(SmartMenuDbContext context) => _context = context;

        public async Task<IEnumerable<AuditLog>> GetAllAsync() =>
            await _context.AuditLogs
                .Include(a => a.ActorUser)
                .OrderByDescending(a => a.OccurredAt)
                .AsNoTracking()
                .ToListAsync();

        public async Task<AuditLog?> GetByIdAsync(long id) =>
            await _context.AuditLogs.Include(a => a.ActorUser)
                .FirstOrDefaultAsync(a => a.AuditId == id);

        public async Task AddAsync(AuditLog log)
        {
            await _context.AuditLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var log = await _context.AuditLogs.FindAsync(id);
            if (log != null)
            {
                _context.AuditLogs.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}
