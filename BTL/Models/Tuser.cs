using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class Tuser
{
    [DisplayName("Tài khoản")]
    public string Username { get; set; } = null!;
    [DisplayName("Mật Khẩu")]
    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;
    [DisplayName("Loại người dùng")]
    public byte? LoaiUser { get; set; }
    [DisplayName("Họ Tên")]
    public string HoTen { get; set; } = null!;
}
