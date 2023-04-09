using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class HoaDonBan
{
    public int Id { get; set; }
    [DisplayName("Số Hóa Đơn")]
    public string SoHd { get; set; } = null!;
    [DisplayName("Ngày Xuất")]
    public DateTime NgayXuat { get; set; }
    [DisplayName("Nhân Viên")]
    public int? IdNv { get; set; }
    [DisplayName("Khách Hàng")]
    public int? IdKh { get; set; }
    [DisplayName("Bàn")]
    public int? IdBan { get; set; }
    [DisplayName("Tổng tiền")]
    public double? TongTien { get; set; }
    [DisplayName("Khuyến mãi")]
    public double? KhuyenMai { get; set; }

    public virtual ICollection<ChitietHdb> ChitietHdbs { get; } = new List<ChitietHdb>();
    [DisplayName("Bàn")]
    public virtual BanAn IdBanNavigation { get; set; } = null!;
    [DisplayName("Tên Khách Hàng")]
    public virtual KhachHang? IdKhNavigation { get; set; }
    [DisplayName("Tên Nhân Viên")]
    public virtual NhanVien IdNvNavigation { get; set; } = null!;
}
