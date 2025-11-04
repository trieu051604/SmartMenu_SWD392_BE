using SmartMenu.Domain.Entities;
using SmartMenu.Infrastructure.Repositories;

namespace SmartMenu.Application.Services
{
    public interface IPlaylistService
    {
        Task<IEnumerable<Playlist>> GetAllAsync();
        Task<Playlist?> GetByIdAsync(long id);
        Task<Playlist> CreateAsync(Playlist playlist);
        Task<Playlist?> UpdateAsync(Playlist playlist);
        Task<bool> DeleteAsync(long id);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _repo;

        public PlaylistService(IPlaylistRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Playlist>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Playlist?> GetByIdAsync(long id) => await _repo.GetByIdAsync(id);

        public async Task<Playlist> CreateAsync(Playlist playlist)
        {
            await _repo.AddAsync(playlist);
            return playlist;
        }

        public async Task<Playlist?> UpdateAsync(Playlist playlist)
        {
            await _repo.UpdateAsync(playlist);
            return playlist;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
