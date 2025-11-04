using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Payment
{
    public long PaymentId { get; set; }

    public string? ExternalRef { get; set; }

    public string OrderId { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
