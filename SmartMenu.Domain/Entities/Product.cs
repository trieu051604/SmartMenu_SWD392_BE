using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Product
{
    public long ProductId { get; set; }

    public string Sku { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Availability { get; set; } = null!;

    public long CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InventorySyncLog> InventorySyncLogs { get; set; } = new List<InventorySyncLog>();

    public virtual ICollection<PlaylistItem> PlaylistItems { get; set; } = new List<PlaylistItem>();

    public virtual ICollection<PosPriceSyncLog> PosPriceSyncLogs { get; set; } = new List<PosPriceSyncLog>();

    public virtual ICollection<ProductPriceVersion> ProductPriceVersions { get; set; } = new List<ProductPriceVersion>();

    public virtual ProductStock? ProductStock { get; set; }

    public virtual ICollection<Promotion> Promotions { get; set; } = new List<Promotion>();
}
