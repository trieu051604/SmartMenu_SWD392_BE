using System;
using System.Collections.Generic;

namespace SmartMenu.Domain.Entities;

public partial class StoreUser
{
    public long StoreUserId { get; set; }

    public long StoreId { get; set; }

    public long UserId { get; set; }

    public virtual Store Store { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
