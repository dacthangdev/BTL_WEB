using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class PhongQl
{
    public int Id { get; set; }
    [DisplayName("Phòng Ban")]
    public string TenPhong { get; set; } = null!;
    public virtual ICollection<NhanVien> NhanViens { get; } = new List<NhanVien>();
}
