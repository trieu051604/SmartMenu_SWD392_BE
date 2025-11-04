using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Device
{
    public long DeviceId { get; set; }

    public long StoreId { get; set; }

    public string Identifier { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? LastSeenAt { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual Store Store { get; set; } = null!;
}
