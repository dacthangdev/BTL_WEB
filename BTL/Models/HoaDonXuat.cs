using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class HoaDonXuat
{
    public int Id { get; set; }
    [DisplayName("Ngày Xuất")]
    public DateTime NgayXuat { get; set; }
    [DisplayName("Nhân Viên")]
    public int IdNv { get; set; }

    public virtual ICollection<ChitietHdx> ChitietHdxes { get; } = new List<ChitietHdx>();
    [DisplayName("Nhân Viên")]
    public virtual NhanVien IdNvNavigation { get; set; } = null!;
}
