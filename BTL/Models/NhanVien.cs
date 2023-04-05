using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models;

public partial class NhanVien
{
    public int Id { get; set; }
    [DisplayName("Ảnh Đại Diện")]
    public string AnhDaiDien { get; set; }
    [DisplayName("Họ và Tên")]
    public string HoTen { get; set; }

    [DisplayName("Ngày sinh")]
    public DateTime NgaySinh { get; set; }
    [DisplayName("Giới Tính")]
    public bool GioiTinh { get; set; }
    [DisplayName("Lương Cơ bản")]
    public int LuongCb { get; set; }

    [DisplayName("Phòng Ban")]
    public int IdPhong { get; set; }
    [DisplayName("Chức vụ")]
    public int IdCv { get; set; }


    public virtual ICollection<HoaDonBan> HoaDonBans { get; } = new List<HoaDonBan>();

    public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; } = new List<HoaDonNhap>();

    public virtual ICollection<HoaDonXuat> HoaDonXuats { get; } = new List<HoaDonXuat>();
    [ForeignKey(nameof(IdCv))]
    public virtual ChucVu IdCvNavigation { get; set; } = null!;
    [ForeignKey(nameof(IdPhong))]
    public virtual PhongQl IdPhongNavigation { get; set; } = null!;
}
