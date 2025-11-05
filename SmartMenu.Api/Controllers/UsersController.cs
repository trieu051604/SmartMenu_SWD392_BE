using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // ✅ GET: api/users?keyword=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var users = await _service.GetAllAsync(keyword);
            return Ok(users.Select(u => new
            {
                u.UserId,
                u.FullName,
                u.Email,
                Role = u.Role?.RoleName,
                u.IsActive,
                u.CreatedAt,
                u.UpdatedAt
            }));
        }

        // ✅ GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _service.GetByIdAsync(id);
            if (user == null) return NotFound($"UserId {id} not found.");

            return Ok(new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.RoleId,
                user.IsActive,
                user.CreatedAt,
                user.UpdatedAt
            });
        }



        // ✅ POST: api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = dto.Password,  // sẽ hash trong service
                RoleId = dto.RoleId,
                IsActive = dto.IsActive
            };

            await _service.CreateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, new
            {
                user.UserId,
                user.FullName,
                user.Email,
                user.RoleId
            });
        }

        // ✅ PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserDto dto)
        {
            var existing = await _service.GetByIdAsync(id);
            if (existing == null) return NotFound($"UserId {id} not found.");

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.RoleId = dto.RoleId;
            existing.IsActive = dto.IsActive;
            if (!string.IsNullOrEmpty(dto.Password))
            {
                existing.PasswordHash = dto.Password; // service sẽ hash
            }

            await _service.UpdateAsync(existing);
            return Ok(new
            {
                existing.UserId,
                existing.FullName,
                existing.Email,
                existing.RoleId,
                existing.IsActive
            });
        }

        // ✅ DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
