using Microsoft.AspNetCore.Mvc;
using SmartMenu.Application.DTOs;
using SmartMenu.Application.Services;
using SmartMenu.Domain.Entities;

namespace SmartMenu.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;

        public ProductsController(IProductService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        // ✅ GET: api/products?keyword=trà
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? keyword)
        {
            var products = await _service.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                products = products.Where(p =>
                    p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                    p.Sku.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            }

            return Ok(products.Select(p => new
            {
                p.ProductId,
                p.Sku,
                p.Name,
                p.Description,
                p.ImageUrl,
                p.Availability,
                p.CategoryId
            }));
        }

        // ✅ GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var product = await _service.GetByIdAsync(id);
            return product == null
                ? NotFound($"ProductId {id} not found.")
                : Ok(new
                {
                    product.ProductId,
                    product.Sku,
                    product.Name,
                    product.Description,
                    product.ImageUrl,
                    product.Availability,
                    product.CategoryId
                });
        }

        // ✅ POST: api/products
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var category = await _categoryService.GetByIdAsync(dto.CategoryId);
            if (category == null)
                return BadRequest($"CategoryId {dto.CategoryId} does not exist.");

            var product = new Product
            {
                Sku = dto.SKU,
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Availability = dto.Availability ?? "available",
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _service.CreateAsync(product);

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, new
            {
                product.ProductId,
                product.Sku,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Availability,
                product.CategoryId
            });
        }

        // ✅ PUT: api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ProductDto dto)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound($"ProductId {id} not found.");

            product.Sku = dto.SKU;
            product.Name = dto.Name;
            product.Description = dto.Description;
            product.ImageUrl = dto.ImageUrl;
            product.Availability = dto.Availability ?? "available";
            product.CategoryId = dto.CategoryId;
            product.UpdatedAt = DateTime.Now;

            await _service.UpdateAsync(product);
            return Ok(new
            {
                product.ProductId,
                product.Sku,
                product.Name,
                product.Description,
                product.ImageUrl,
                product.Availability,
                product.CategoryId
            });
        }

        // ✅ DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound($"ProductId {id} not found.");

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
