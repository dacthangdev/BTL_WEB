using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChitietTtb
{
    public int IdBan { get; set; }

    public int IdTt { get; set; }

    public DateTime? ThoiGian { get; set; }

    public virtual BanAn IdBanNavigation { get; set; } = null!;

    public virtual TinhTrang IdTtNavigation { get; set; } = null!;
}
