using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<AuditLog?> GetByIdAsync(long id);
        Task<AuditLog> CreateAsync(AuditLog log);
        Task<bool> DeleteAsync(long id);
    }

    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repo;
        public AuditLogService(IAuditLogRepository repo) => _repo = repo;

        public async Task<IEnumerable<AuditLog>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<AuditLog?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);
        public async Task<AuditLog> CreateAsync(AuditLog log)
        {
            await _repo.AddAsync(log);
            return log;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
