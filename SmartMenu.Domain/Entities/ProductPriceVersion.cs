using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class ProductPriceVersion
{
    public long PriceVersionId { get; set; }

    public long ProductId { get; set; }

    public decimal Price { get; set; }

    public string Currency { get; set; } = null!;

    public string Source { get; set; } = null!;

    public DateTime EffectiveFrom { get; set; }

    public DateTime? EffectiveTo { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual Product Product { get; set; } = null!;
}
