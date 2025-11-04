using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class ProductStock
{
    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public string Status { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
