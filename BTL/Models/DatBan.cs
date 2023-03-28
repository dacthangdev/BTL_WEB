using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class DatBan
{
    public DateTime ThoiGian { get; set; }

    public int Sl { get; set; }

    public int IdBan { get; set; }

    public int IdKh { get; set; }

    public virtual BanAn IdBanNavigation { get; set; } = null!;

    public virtual KhachHang IdKhNavigation { get; set; } = null!;
}
