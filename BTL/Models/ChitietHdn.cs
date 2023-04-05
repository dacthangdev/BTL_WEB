using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class ChitietHdn
{
    [DisplayName("Số lượng")]
    public int Sl { get; set; }
    [DisplayName("Hàng Hóa")]
    public int IdHh { get; set; }
    [DisplayName("Hóa Đơn Nhập")]
    public int IdHdn { get; set; }
    [DisplayName("Hóa Đơn Nhập")]
    public virtual HoaDonNhap IdHdnNavigation { get; set; } = null!;
    [DisplayName("Hàng Hóa")]
    public virtual HangHoa IdHhNavigation { get; set; } = null!;
}
