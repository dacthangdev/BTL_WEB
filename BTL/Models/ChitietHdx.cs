using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class ChitietHdx
{
    [DisplayName("Số lượng")]
    public int Sl { get; set; }
    [DisplayName("Hàng hóa")]
    public int IdHh { get; set; }
    [DisplayName("Hóa Đơn")]
    public int IdHdx { get; set; }
    [DisplayName("Hóa đơn")]
    public virtual HoaDonXuat IdHdxNavigation { get; set; } = null!;
    [DisplayName("Hàng hóa")]
    public virtual HangHoa IdHhNavigation { get; set; } = null!;
}
