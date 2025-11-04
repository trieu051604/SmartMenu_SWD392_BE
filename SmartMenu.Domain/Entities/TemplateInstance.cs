using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class TemplateInstance
{
    public long TemplateInstanceId { get; set; }

    public long TemplateId { get; set; }

    public string Name { get; set; } = null!;

    public string? BindingJson { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();

    public virtual MenuTemplate Template { get; set; } = null!;
}
