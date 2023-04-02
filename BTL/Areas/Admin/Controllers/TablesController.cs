using BTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Tables")]
    public class TablesController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();
        [Route("TableHoaDonBan")]
        public IActionResult TableHoaDonBan()
        {
            List<HoaDonBan> list = db.HoaDonBans.ToList();
            return View(list);
        }

        [Route("TableHoaDonNhap")]
        public IActionResult TableHoaDonNhap()
        {
            List<HoaDonBan> list = db.HoaDonBans.ToList();
            return View(list);
        }

        [Route("TableHoaDonXuat")]
        public IActionResult TableHoaDonXuat()
        {
            List<HoaDonXuat> list = db.HoaDonXuats.ToList();
            return View(list);
        }

        [Route("TableNhanVien")]
        public IActionResult TableNhanVien()
        {
            List<NhanVien> list = db.NhanViens.ToList();
            return View(list);
        }

        [Route("TableKhachHang")]
        public IActionResult TableKhachHang()
        {
            List<KhachHang> list = db.KhachHangs.ToList();
            return View(list);
        }

        [Route("TableNhaCC")]
        public IActionResult TableNhaCC()
        {
            List<NhaCungCap> list = db.NhaCungCaps.ToList();
            return View(list);
        }

        [Route("TablePhongBan")]
        public IActionResult TablePhongBan()
        {
            List<PhongQl> list = db.PhongQls.ToList();
            return View(list);
        }
    }
}
