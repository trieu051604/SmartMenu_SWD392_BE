namespace SmartMenu.Application.DTOs
{
    public class TemplateDto
    {
        public string Name { get; set; } = null!;
        public string LayoutJson { get; set; } = null!;
        public string? Metadata { get; set; }
        public long CreatedBy { get; set; }
    }
}
