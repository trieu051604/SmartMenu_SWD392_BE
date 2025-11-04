using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public long RoleId { get; set; } 

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<MenuTemplate> MenuTemplates { get; set; } = new List<MenuTemplate>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual ICollection<ProductPriceVersion> ProductPriceVersions { get; set; } = new List<ProductPriceVersion>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();

    public virtual ICollection<Store> Stores { get; set; } = new List<Store>();

    public virtual ICollection<TemplateInstance> TemplateInstances { get; set; } = new List<TemplateInstance>();
}
