using System;
using System.Collections.Generic;

namespace BTL.Models;

public partial class DatBan
{
    public int Id { get; set; }
    public string? Name_KH { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime? NgayDat { get; set; }
    public string? Gio_Nhan { get; set; }
    public string? Sl { get; set; }

}
