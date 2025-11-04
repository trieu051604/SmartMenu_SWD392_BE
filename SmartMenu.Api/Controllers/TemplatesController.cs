using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TemplatesController : ControllerBase
    {
        private readonly ITemplateService _service;

        public TemplatesController(ITemplateService service)
        {
            _service = service;
        }

        // ✅ GET: api/templates?keyword=menu
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var templates = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                templates = templates.Where(t =>
                    t.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(templates.Select(t => new
            {
                t.TemplateId,
                t.Name,
                t.LayoutJson,
                t.Metadata,
                t.CreatedBy,
                t.CreatedAt,
                t.UpdatedAt
            }));
        }

        // ✅ GET: api/templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var template = await _service.GetByIdAsync(id);
            if (template == null) return NotFound($"TemplateId {id} not found.");
            return Ok(template);
        }

        // ✅ POST: api/templates
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TemplateDto dto)
        {
            var template = new MenuTemplate
            {
                Name = dto.Name,
                LayoutJson = dto.LayoutJson,
                Metadata = dto.Metadata,
                CreatedBy = dto.CreatedBy,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _service.CreateAsync(template);
            return CreatedAtAction(nameof(GetById), new { id = template.TemplateId }, new
            {
                template.TemplateId,
                template.Name,
                template.LayoutJson,
                template.Metadata,
                template.CreatedBy
            });
        }

        // ✅ PUT: api/templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] TemplateDto dto)
        {
            var template = await _service.GetByIdAsync(id);
            if (template == null) return NotFound($"TemplateId {id} not found.");

            template.Name = dto.Name;
            template.LayoutJson = dto.LayoutJson;
            template.Metadata = dto.Metadata;
            template.UpdatedAt = DateTime.Now;

            await _service.UpdateAsync(template);
            return Ok(template);
        }

        // ✅ DELETE: api/templates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var template = await _service.GetByIdAsync(id);
            if (template == null) return NotFound($"TemplateId {id} not found.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
