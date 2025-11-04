using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class MenuTemplate
{
    public long TemplateId { get; set; }

    public string Name { get; set; } = null!;

    public string LayoutJson { get; set; } = null!;

    public string? Metadata { get; set; }

    public long CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<TemplateInstance> TemplateInstances { get; set; } = new List<TemplateInstance>();
}
