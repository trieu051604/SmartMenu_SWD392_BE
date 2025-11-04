using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class PosPriceSyncLog
{
    public long SyncId { get; set; }

    public long ProductId { get; set; }

    public string PayloadJson { get; set; } = null!;

    public DateTime ProcessedAt { get; set; }

    public string Result { get; set; } = null!;

    public string? ErrorMessage { get; set; }

    public virtual Product Product { get; set; } = null!;
}
