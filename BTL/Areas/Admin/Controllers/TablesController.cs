using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Tables")]
    [Route("Forms")]
    public class TablesController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();
        [Route("TableHoaDonBan")]
        public IActionResult TableHoaDonBan()
        {
            List<HoaDonBan> list = db.HoaDonBans.Include(x=>x.IdBanNavigation).Include(x=>x.IdKhNavigation).Include(x=> x.IdNvNavigation).ToList();
            return View(list);
        }

        [Route("TableChiTietHDB")]
        public IActionResult TableChiTietHDB(int idHDB)
        {
            List<ChitietHdb> list = db.ChitietHdbs.Where(x=>x.IdHdb == idHDB).Include(x => x.IdHdbNavigation).Include(x => x.IdMenuNavigation).ToList();
            ViewBag.IdHdb = idHDB;
            return View(list);
        }
        [Route("TableHoaDonNhap")]
        public IActionResult TableHoaDonNhap()
        {
            List<HoaDonNhap> list = db.HoaDonNhaps.Include(x => x.IdNvNavigation).ToList();
            return View(list);
        }
        [Route("TableChiTietHDN")]
        public IActionResult TableChiTietHDN(int idHDN)
        {
            ViewBag.IdHdn = idHDN;
            List<ChitietHdn> list = db.ChitietHdns.Where(x=>x.IdHdn == idHDN).Include(x => x.IdHdnNavigation).Include(x => x.IdHhNavigation).ToList();
            return View(list);
        }

        [Route("TableHoaDonXuat")]
        public IActionResult TableHoaDonXuat()
        {
            List<HoaDonXuat> list = db.HoaDonXuats.Include(x=> x.IdNvNavigation).ToList();
            return View(list);
        }

        [Route("TableChiTietHDX")]
        public IActionResult TableChiTietHDX(int idHDX)
        {
            ViewBag.IdHdx = idHDX;
            List<ChitietHdx> list = db.ChitietHdxes.Where(x => x.IdHdx == idHDX).Include(x => x.IdHdxNavigation).Include(x => x.IdHhNavigation).ToList();
            return View(list);
        }

        [Route("TableNhanVien")]
        public IActionResult TableNhanVien()
        {
            var list = db.NhanViens.Include(x=>x.IdCvNavigation).Include(x=>x.IdPhongNavigation);
            return View(list.ToList());
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

        [Route("TableHangHoa")]
        public IActionResult TableHangHoa()
        {
            List<HangHoa> list = db.HangHoas.Include(x=>x.IdNhaCcNavigation).ToList();
            return View(list);
        }
        [Route("TableMenu")]
        public IActionResult TableMenu()
        {
            List<Menu> list = db.Menus.Include(x => x.IdLoaiNavigation).ToList();
            return View(list);
        }
    }
}
