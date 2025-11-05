using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _service;

        public DevicesController(IDeviceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var devices = await _service.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                devices = devices.Where(d =>
                    d.Identifier.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(devices.Select(d => new
            {
                d.DeviceId,
                d.StoreId,
                d.Identifier,
                d.Status,
                d.LastSeenAt
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var device = await _service.GetByIdAsync(id);
            return device == null ? NotFound() : Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DeviceDto dto)
        {
            var device = new Device
            {
                StoreId = dto.StoreId,
                Identifier = dto.Identifier,
                Status = dto.Status ?? "offline",
                LastSeenAt = DateTime.Now
            };

            await _service.CreateAsync(device);
            return CreatedAtAction(nameof(GetById), new { id = device.DeviceId }, device);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] DeviceDto dto)
        {
            var device = await _service.GetByIdAsync(id);
            if (device == null) return NotFound();

            device.StoreId = dto.StoreId;
            device.Identifier = dto.Identifier;
            device.Status = dto.Status ?? "offline";
            device.LastSeenAt = DateTime.Now;

            await _service.UpdateAsync(device);
            return Ok(device);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
