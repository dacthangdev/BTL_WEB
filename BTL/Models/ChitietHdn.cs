using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChitietHdn
{
    public int Sl { get; set; }

    public int IdHh { get; set; }

    public int IdHdn { get; set; }

    public virtual HoaDonNhap IdHdnNavigation { get; set; } = null!;

    public virtual HangHoa IdHhNavigation { get; set; } = null!;
}
