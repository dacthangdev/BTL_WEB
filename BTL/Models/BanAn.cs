using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class BanAn
{
    public int Id { get; set; }
    [DisplayName("Số lượng ghế")]
    public int Slghe { get; set; }
    [DisplayName("Tầng")]
    public int IdTang { get; set; }

    public virtual ICollection<ChitietTtb> ChitietTtbs { get; } = new List<ChitietTtb>();


    public virtual ICollection<HoaDonBan> HoaDonBans { get; } = new List<HoaDonBan>();

    public virtual Tang IdTangNavigation { get; set; } = null!;
}
