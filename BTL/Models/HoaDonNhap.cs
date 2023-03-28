using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HoaDonNhap
{
    public int Id { get; set; }

    public DateTime NgayNhap { get; set; }

    public int IdNv { get; set; }

    public virtual ICollection<ChitietHdn> ChitietHdns { get; } = new List<ChitietHdn>();

    public virtual NhanVien IdNvNavigation { get; set; } = null!;
}
