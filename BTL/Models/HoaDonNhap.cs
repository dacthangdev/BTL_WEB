using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class HoaDonNhap
{
    public int Id { get; set; }
    [DisplayName("Ngày Nhập")]
    public DateTime NgayNhap { get; set; }
    [DisplayName("Nhân Viên")]
    public int IdNv { get; set; }

    public virtual ICollection<ChitietHdn> ChitietHdns { get; } = new List<ChitietHdn>();
    [DisplayName("Nhân Viên")]
    public virtual NhanVien IdNvNavigation { get; set; } = null!;
}
