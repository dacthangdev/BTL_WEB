using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BTL.Models;

public partial class HangHoa
{
    public int Id { get; set; }
    [DisplayName("Tên Hàng Hóa")]
    public string TenHh { get; set; } = null!;
    [DisplayName("Số Lượng")]
    public int Sl { get; set; }
    [DisplayName("Ngày Nhập")]
    public DateTime NgayNhap { get; set; }
    [DisplayName("Nhà Cung Cấp")]
    public int IdNhaCc { get; set; }

    public virtual ICollection<ChitietHdn> ChitietHdns { get; } = new List<ChitietHdn>();

    public virtual ICollection<ChitietHdx> ChitietHdxes { get; } = new List<ChitietHdx>();
    [DisplayName("Nhà Cung Cấp")]
    public virtual NhaCungCap IdNhaCcNavigation { get; set; } = null!;
}
