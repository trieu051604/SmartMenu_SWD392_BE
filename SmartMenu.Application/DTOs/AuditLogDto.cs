namespace SmartMenu.Application.DTOs
{
    public class AuditLogDto
    {
        public long ActorUserId { get; set; }
        public string Action { get; set; } = null!;
        public string EntityType { get; set; } = null!;
        public long? EntityId { get; set; }
        public string? Details { get; set; }
        public string? Status { get; set; }
    }
}
