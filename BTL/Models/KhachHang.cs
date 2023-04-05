using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class KhachHang
{
    public int Id { get; set; }
    [DisplayName("Họ tên")]
    public string HoTen { get; set; } = null!;
    [DisplayName("Số điện thoại")]
    public string Sdt { get; set; } = null!;
    [DisplayName("Email")]
    public string Email { get; set; } = null!;
    [DisplayName("Ảnh")]
    public string Img { get; set; }

    public virtual ICollection<HoaDonBan> HoaDonBans { get; } = new List<HoaDonBan>();
}
