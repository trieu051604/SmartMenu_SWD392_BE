using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class ErrorLog
{
    public long ErrorId { get; set; }

    public DateTime OccurredAt { get; set; }

    public string Component { get; set; } = null!;

    public string Severity { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? ContextJson { get; set; }

    public bool Resolved { get; set; }
}
