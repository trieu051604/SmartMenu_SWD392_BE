namespace SmartMenu.Application.DTOs
{
    public class PlaylistDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public long CreatedBy { get; set; }
        public string? Status { get; set; } = "active";
    }
}
