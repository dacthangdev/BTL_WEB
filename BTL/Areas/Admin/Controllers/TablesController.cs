using BTL.Models;
using BTL.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Tables")]
    [Route("Forms")]
    [Authentication]
    public class TablesController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();
        static string anhmenu;
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
        [Route("EditMenu")]
        [HttpGet]
        public IActionResult EditMenu(int Id)
        {
            ViewBag.Id = new SelectList(db.Menus.OrderBy(x => x.Id), "Id", "TenMon");
            ViewBag.IdLoai = new SelectList(db.LoaiMonAns.OrderBy(x => x.Id), "Id", "TenLoai");
            var sp = db.Menus.Find(Id);
            anhmenu = sp.Anh;
            return View(sp);
        }
        [Route("EditMenu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMenu(Menu sp, IFormFile formFile)
        {
            if (formFile != null)
            {
                Guid guid = Guid.NewGuid();

                string newfileName = guid.ToString();

                string fileextention = Path.GetExtension(formFile.FileName);

                string fileName = newfileName + fileextention;

                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages\\anhWeb", fileName);

                var stream = new FileStream(uploadpath, FileMode.Create);

                formFile.CopyToAsync(stream);
                sp.Anh = fileName.ToString();
            }
            else
            {
                sp.Anh = anhmenu;
            }
            try
            {
                sp.IdLoaiNavigation = db.LoaiMonAns.Find(sp.Id);
                db.Entry(sp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TableMenu", "Tables");
            }
            catch (Exception ex)
            {
                return View(sp);
            }
        }
        [Route("DeleteMenu")]
        [HttpDelete]
        public bool DeleteMenu(int Id)
        {
            try
            {
                Menu menu = db.Menus.Find(Id);
                if (menu != null)
                {
                    db.Menus.Remove(menu);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return false;
        }
        [Route("TableBlog")]
        public IActionResult TableBlog()
        {
            List<Blog> list = db.Blogs.ToList();
            return View(list);
        }

        [Route("TableDatBan")]
        public IActionResult TableDatBan()
        {
            List<DatBan> list = db.DatBans.Where(x=> x.Ngaydat >= DateTime.Now).ToList();
            return View(list);
        }

        [Route("TableUser")]
        public IActionResult TableUser()
        {
            List<Tuser> list = db.Tusers.ToList();
            return View(list);
        }

        [Route("TableContact")]
        public IActionResult TableContact()
        {
            List<Contact> list = db.Contacts.ToList();
            return View(list);
        }
    }
}
