using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class NhaCungCap
{
    public int Id { get; set; }
    [DisplayName("Tên Nhà cung cấp")]
    public string TenNcc { get; set; } = null!;

    public virtual ICollection<HangHoa> HangHoas { get; } = new List<HangHoa>();
}
