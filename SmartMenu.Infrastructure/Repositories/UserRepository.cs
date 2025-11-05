using SmartMenu.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        // Auth (đăng nhập / đăng ký)
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);

        // Quản lý User (Admin)
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(long id);
        Task UpdateAsync(User user);
        Task DeleteAsync(long id);
    }

    public class UserRepository : IUserRepository
    {
        private readonly SmartMenuDbContext _context;

        public UserRepository(SmartMenuDbContext context)
        {
            _context = context;
        }

        // 🔹 Dùng cho AuthService
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // 🔹 Dùng cho UserController (Admin)
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
