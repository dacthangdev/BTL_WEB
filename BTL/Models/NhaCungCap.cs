using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class NhaCungCap
{
    public int Id { get; set; }

    public string TenNcc { get; set; } = null!;

    public virtual ICollection<HangHoa> HangHoas { get; } = new List<HangHoa>();
}
