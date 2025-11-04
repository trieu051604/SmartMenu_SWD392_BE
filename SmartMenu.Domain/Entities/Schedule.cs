using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Schedule
{
    public long ScheduleId { get; set; }

    public long PlaylistId { get; set; }

    public long DeviceId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Recurrence { get; set; }

    public int Priority { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Device Device { get; set; } = null!;

    public virtual Playlist Playlist { get; set; } = null!;
}
