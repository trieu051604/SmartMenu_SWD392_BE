using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Playlist
{
    public long PlaylistId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public long CreatedBy { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}
