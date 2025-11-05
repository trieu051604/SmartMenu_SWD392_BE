using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace SmartMenu.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(string? keyword = null);
        Task<User?> GetByIdAsync(long id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(long id);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? keyword = null)
        {
            var users = await _repo.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                users = users.Where(u =>
                    u.FullName.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return users;
        }

        public async Task<User?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {
            user.PasswordHash = HashPassword(user.PasswordHash);
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _repo.AddAsync(user);
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTime.Now;
            await _repo.UpdateAsync(user);
            return user;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
    