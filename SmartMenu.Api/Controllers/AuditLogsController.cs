using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _service;

        public AuditLogsController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var logs = await _service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                logs = logs.Where(l =>
                    l.Action.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    l.EntityType.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(logs.Select(l => new
            {
                l.AuditId,
                l.ActorUserId,
                l.Action,
                l.EntityType,
                l.EntityId,
                l.OccurredAt,
                l.Status
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AuditLogDto dto)
        {
            var log = new AuditLog
            {
                ActorUserId = dto.ActorUserId,
                Action = dto.Action,
                EntityType = dto.EntityType,
                EntityId = dto.EntityId,
                Details = dto.Details,
                Status = dto.Status,
                OccurredAt = DateTime.Now
            };

            await _service.CreateAsync(log);
            return CreatedAtAction(nameof(GetAll), new { id = log.AuditId }, log);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
