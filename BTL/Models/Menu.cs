using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class Menu
{
    public int Id { get; set; }
    [DisplayName("Tên Món")]
    public string TenMon { get; set; } = null!;
    [DisplayName("Giá Tiền")]
    public int Gia { get; set; }
    [DisplayName("Ảnh")]
    public string Anh { get; set; } = null!;
    [DisplayName("Loại")]
    public int IdLoai { get; set; }

    public virtual ICollection<ChitietHdb> ChitietHdbs { get; } = new List<ChitietHdb>();
    [DisplayName("Loại")]
    public virtual LoaiMonAn IdLoaiNavigation { get; set; } = null!;
}
