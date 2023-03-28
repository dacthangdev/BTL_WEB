using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HoaDonBan
{
    public int Id { get; set; }

    public string SoHd { get; set; } = null!;

    public DateTime NgayXuat { get; set; }

    public int IdNv { get; set; }

    public int? IdKh { get; set; }

    public int IdBan { get; set; }

    public virtual ICollection<ChitietHdb> ChitietHdbs { get; } = new List<ChitietHdb>();

    public virtual BanAn IdBanNavigation { get; set; } = null!;

    public virtual KhachHang? IdKhNavigation { get; set; }

    public virtual NhanVien IdNvNavigation { get; set; } = null!;
}
