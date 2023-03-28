using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class LoaiMonAn
{
    public int Id { get; set; }

    public string TenLoai { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; } = new List<Menu>();
}
