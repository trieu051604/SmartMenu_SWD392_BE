using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class PlaylistItem
{
    public long PlaylistItemId { get; set; }

    public long PlaylistId { get; set; }

    public long? ProductId { get; set; }

    public long? TemplateInstanceId { get; set; }

    public int DisplayOrder { get; set; }

    public int DurationSeconds { get; set; }

    public virtual Playlist Playlist { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual TemplateInstance? TemplateInstance { get; set; }
}
