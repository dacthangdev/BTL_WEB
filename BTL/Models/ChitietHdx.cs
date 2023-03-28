using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class ChitietHdx
{
    public int Sl { get; set; }

    public int IdHh { get; set; }

    public int IdHdx { get; set; }

    public virtual HoaDonXuat IdHdxNavigation { get; set; } = null!;

    public virtual HangHoa IdHhNavigation { get; set; } = null!;
}
