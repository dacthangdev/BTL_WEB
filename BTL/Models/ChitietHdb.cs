using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class ChitietHdb
{
    [DisplayName("Số lượng")]
    public int Sl { get; set; }
    [DisplayName("Giá Tiền")]
    public int GiaTien { get; set; }
    [DisplayName("Khuyến Mại")]
    public double KhuyenMai { get; set; }
    [DisplayName("Món Ăn")]
    public int IdMenu { get; set; }
    [DisplayName("Hóa Đơn")]
    public int IdHdb { get; set; }
    [DisplayName("Hóa Đơn")]
    public virtual HoaDonBan IdHdbNavigation { get; set; } = null!;
    [DisplayName("Món Ăn")]
    public virtual Menu IdMenuNavigation { get; set; } = null!;
}
