using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Promotion
{
    public long PromotionId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
