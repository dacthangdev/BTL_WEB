using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class TinhTrang
{
    public int Id { get; set; }

    public string TrangThat { get; set; } = null!;

    public virtual ICollection<ChitietTtb> ChitietTtbs { get; } = new List<ChitietTtb>();
}
