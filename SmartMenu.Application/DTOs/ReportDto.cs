namespace SmartMenu.Application.DTOs
{
    public class ReportDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Type { get; set; } = null!;
        public string? DataJson { get; set; }
        public long CreatedBy { get; set; }
    }
}
