namespace SmartMenu.Application.DTOs
{
    public class ProductCreateDto
    {
        public string SKU { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Availability { get; set; } = "available";
        public long CategoryId { get; set; }
    }
}
