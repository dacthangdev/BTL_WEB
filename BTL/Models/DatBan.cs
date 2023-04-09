using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class DatBan
{
    [DisplayName("Tên Khách Hàng")]
    public string? NameKh { get; set; }
    [DisplayName("Tên người đặt")]
    public string? Email { get; set; }
    [DisplayName("Số điện thoại")]
    public string? Phone { get; set; }
    [DisplayName("Ngày đặt")]
    public DateTime? Ngaydat { get; set; }
    [DisplayName("Giờ nhận")]
    public string? GioNhan { get; set; }

    public int Id { get; set; }
    [DisplayName("Số lượng người")]
    public string? Sl { get; set; }
}
