using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public DateTime OccurredAt { get; set; }

    public long? ActorUserId { get; set; }

    public string Action { get; set; } = null!;

    public string? EntityType { get; set; }

    public long? EntityId { get; set; }

    public string? Details { get; set; }

    public string Status { get; set; } = null!;

    public virtual User? ActorUser { get; set; }
}
