namespace SmartMenu.Application.DTOs
{
    public class DeviceDto
    {
        public long StoreId { get; set; }
        public string Identifier { get; set; } = null!;
        public string? Status { get; set; } = "offline";
    }
}
