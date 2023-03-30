using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class ChucVu
{
    public int Id { get; set; }
    [DisplayName("Chức Vụ")]
    public string TenCv { get; set; } = null!;
    [DisplayName("Hệ số lương")]
    public double Hsl { get; set; }

    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();
}
