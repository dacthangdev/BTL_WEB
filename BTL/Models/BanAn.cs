using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class BanAn
{
    public int Id { get; set; }

    public int Slghe { get; set; }

    public int IdTang { get; set; }

    public virtual ICollection<ChitietTtb> ChitietTtbs { get; } = new List<ChitietTtb>();

    public virtual ICollection<DatBan> DatBans { get; } = new List<DatBan>();

    public virtual ICollection<HoaDonBan> HoaDonBans { get; } = new List<HoaDonBan>();

    public virtual Tang IdTangNavigation { get; set; } = null!;
}
