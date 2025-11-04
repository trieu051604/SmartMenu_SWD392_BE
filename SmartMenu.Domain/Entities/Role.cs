using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class Role
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Permissions { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
