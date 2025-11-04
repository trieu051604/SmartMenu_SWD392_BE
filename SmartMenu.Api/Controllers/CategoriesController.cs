using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        // ✅ GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var categories = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                categories = categories.Where(c => c.CategoryName
                    .Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(categories.Select(c => new
            {
                c.CategoryId,
                c.CategoryName
            }));
        }

        // ✅ GET: api/categories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var category = await _service.GetByIdAsync(id);
            return category == null
                ? NotFound($"CategoryId {id} not found.")
                : Ok(new { category.CategoryId, category.CategoryName });
        }

        // ✅ POST: api/categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                return BadRequest("CategoryName is required.");

            var category = new Category { CategoryName = dto.CategoryName };
            await _service.CreateAsync(category);

            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, new
            {
                category.CategoryId,
                category.CategoryName
            });
        }

        // ✅ PUT: api/categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] CategoryDto dto)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound($"CategoryId {id} not found.");

            category.CategoryName = dto.CategoryName;
            await _service.UpdateAsync(category);

            return Ok(new { category.CategoryId, category.CategoryName });
        }

        // ✅ DELETE: api/categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await _service.GetByIdAsync(id);
            if (category == null) return NotFound($"CategoryId {id} not found.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
