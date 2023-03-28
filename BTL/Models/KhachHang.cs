using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class KhachHang
{
    public int Id { get; set; }

    public string HoTen { get; set; } = null!;

    public string Sdt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<DatBan> DatBans { get; } = new List<DatBan>();

    public virtual ICollection<HoaDonBan> HoaDonBans { get; } = new List<HoaDonBan>();
}
