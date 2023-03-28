using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class HangHoa
{
    public int Id { get; set; }

    public string TenHh { get; set; } = null!;

    public int Sl { get; set; }

    public DateTime NgayNhap { get; set; }

    public int IdNhaCc { get; set; }

    public virtual ICollection<ChitietHdn> ChitietHdns { get; } = new List<ChitietHdn>();

    public virtual ICollection<ChitietHdx> ChitietHdxes { get; } = new List<ChitietHdx>();

    public virtual NhaCungCap IdNhaCcNavigation { get; set; } = null!;
}
