using Microsoft.EntityFrameworkCore;
using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Data;

namespace SmartMenu.Infrastructure.Repositories
{
    public interface IPlaylistRepository
    {
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task<Playlist?> GetByIdAsync(long id);
        Task AddAsync(Playlist playlist);
        Task UpdateAsync(Playlist playlist);
        Task DeleteAsync(long id);
    }

    public class PlaylistRepository : IPlaylistRepository
    {
        private readonly SmartMenuDbContext _context;

        public PlaylistRepository(SmartMenuDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync() =>
            await _context.Playlists.AsNoTracking().ToListAsync();

        public async Task<Playlist?> GetByIdAsync(long id) =>
            await _context.Playlists.FindAsync(id);

        public async Task AddAsync(Playlist playlist)
        {
            await _context.Playlists.AddAsync(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Playlist playlist)
        {
            _context.Playlists.Update(playlist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist != null)
            {
                _context.Playlists.Remove(playlist);
                await _context.SaveChangesAsync();
            }
        }
    }
}
