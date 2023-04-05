using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class Tang
{
    public int Id { get; set; }
    [DisplayName("Tầng")]
    public string TenTang { get; set; } = null!;

    public virtual ICollection<BanAn> BanAns { get; } = new List<BanAn>();
}
