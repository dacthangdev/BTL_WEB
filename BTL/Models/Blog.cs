using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class Blog
{
    public int Id { get; set; }

    public string TacGia { get; set; } = null!;

    public string TieuDe { get; set; } = null!;

    public string NoiDung { get; set; } = null!;

    public string Anh { get; set; } = null!;

    public string Cmt { get; set; } = null!;

    public string NguoiCmt { get; set; } = null!;

    public DateTime NgayDang { get; set; }
}
