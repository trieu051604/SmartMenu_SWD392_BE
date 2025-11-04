using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _service;

        public PlaylistsController(IPlaylistService service)
        {
            _service = service;
        }

        // ✅ GET: api/playlists?keyword=summer
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var playlists = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                playlists = playlists.Where(p =>
                    p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(playlists.Select(p => new
            {
                p.PlaylistId,
                p.Name,
                p.Description,
                p.Status,
                p.CreatedBy,
                p.CreatedAt,
                p.UpdatedAt
            }));
        }

        // ✅ GET: api/playlists/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var playlist = await _service.GetByIdAsync(id);
            return playlist == null
                ? NotFound($"PlaylistId {id} not found.")
                : Ok(playlist);
        }

        // ✅ POST: api/playlists
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlaylistDto dto)
        {
            var playlist = new Playlist
            {
                Name = dto.Name,
                Description = dto.Description,
                Status = dto.Status ?? "active",
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _service.CreateAsync(playlist);

            return CreatedAtAction(nameof(GetById), new { id = playlist.PlaylistId }, new
            {
                playlist.PlaylistId,
                playlist.Name,
                playlist.Description,
                playlist.Status,
                playlist.CreatedBy
            });
        }

        // ✅ PUT: api/playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] PlaylistDto dto)
        {
            var playlist = await _service.GetByIdAsync(id);
            if (playlist == null) return NotFound($"PlaylistId {id} not found.");

            playlist.Name = dto.Name;
            playlist.Description = dto.Description;
            playlist.Status = dto.Status ?? "active";
            playlist.UpdatedAt = DateTime.Now;

            await _service.UpdateAsync(playlist);
            return Ok(playlist);
        }

        // ✅ DELETE: api/playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var playlist = await _service.GetByIdAsync(id);
            if (playlist == null) return NotFound($"PlaylistId {id} not found.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
