using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChitietHdb
{
    public int Sl { get; set; }

    public int GiaTien { get; set; }

    public double KhuyenMai { get; set; }

    public int IdMenu { get; set; }

    public int IdHdb { get; set; }

    public virtual HoaDonBan IdHdbNavigation { get; set; } = null!;

    public virtual Menu IdMenuNavigation { get; set; } = null!;
}
