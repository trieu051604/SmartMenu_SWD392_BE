namespace SmartMenu.Application.DTOs
{
    public class ScheduleDto
    {
        public long PlaylistId { get; set; }
        public long DeviceId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Recurrence { get; set; }
        public int Priority { get; set; } = 1;
        public long CreatedBy { get; set; }
    }
}
