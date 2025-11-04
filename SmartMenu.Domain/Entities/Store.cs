using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Store
{
    public long StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? Address { get; set; }

    public long? ManagerId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Device> Devices { get; set; } = new List<Device>();

    public virtual User? Manager { get; set; }

    public virtual ICollection<StoreUser> StoreUsers { get; set; } = new List<StoreUser>();
}
