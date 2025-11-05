using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _service;

        public SchedulesController(IScheduleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var schedules = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                schedules = schedules.Where(s =>
                    s.Playlist.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(schedules.Select(s => new
            {
                s.ScheduleId,
                s.PlaylistId,
                s.DeviceId,
                s.StartTime,
                s.EndTime,
                s.Recurrence,
                s.Priority,
                s.CreatedBy,
                s.CreatedAt
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var schedule = await _service.GetByIdAsync(id);
            return schedule == null ? NotFound() : Ok(schedule);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ScheduleDto dto)
        {
            var schedule = new Schedule
            {
                PlaylistId = dto.PlaylistId,
                DeviceId = dto.DeviceId,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Recurrence = dto.Recurrence,
                Priority = dto.Priority,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now
            };

            await _service.CreateAsync(schedule);
            return CreatedAtAction(nameof(GetById), new { id = schedule.ScheduleId }, schedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ScheduleDto dto)
        {
            var schedule = await _service.GetByIdAsync(id);
            if (schedule == null) return NotFound();

            schedule.PlaylistId = dto.PlaylistId;
            schedule.DeviceId = dto.DeviceId;
            schedule.StartTime = dto.StartTime;
            schedule.EndTime = dto.EndTime;
            schedule.Recurrence = dto.Recurrence;
            schedule.Priority = dto.Priority;
            schedule.CreatedAt = DateTime.Now;

            await _service.UpdateAsync(schedule);
            return Ok(schedule);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
