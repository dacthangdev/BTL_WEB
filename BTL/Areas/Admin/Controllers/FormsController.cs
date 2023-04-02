using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;


namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Forms")]
    public class FormsController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();

        [Route("NhanVien")]
        [HttpGet]
        public IActionResult AddNhanVien()
        {
            ViewBag.IdPhong = new SelectList(db.PhongQls.OrderBy(x => x.Id), "Id", "TenPhong");
            ViewBag.IdCv = new SelectList(db.ChucVus.OrderBy(x => x.Id), "Id", "TenCv");
            return View();
        }
        [Route("NhanVien")]
        [HttpPost]
        public IActionResult AddNhanVien(NhanVien nv)
        {
            try
            {
                nv.IdCvNavigation = db.ChucVus.Find(nv.IdCv);
                nv.IdPhongNavigation = db.PhongQls.Find(nv.IdPhong);
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(nv);
            }
        }
        [Route("EditNhanVien")]
        public IActionResult EditNhanVien()
        {
            return View();
        }
        [Route("DeleteNhanVien")]
        public IActionResult DeleteNhanVien()
        {
            return View();
        }
        [Route("KhachHang")]
        [HttpGet]
        public IActionResult AddKhachHang()
        {
            return View();
        }
        [Route("KhachHang")]
        [HttpPost]
        public IActionResult AddKhachHang(KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }
        [Route("EditKhachHang")]
        public IActionResult EditKhachHang(int id)
        {
            return View();
        }

        [Route("ChucVu")]
        [HttpGet]
        public IActionResult AddChucVu()
        {
            return View();
        }
        [Route("ChucVu")]
        [HttpPost]
        public IActionResult AddChucVu(ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                db.ChucVus.Add(chucVu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chucVu);
        }
        [Route("PhongBan")]
        [HttpGet]
        public IActionResult AddPhongBan()
        {
            return View();
        }
        [Route("PhongBan")]
        [HttpPost]
        public IActionResult AddPhongBan(PhongQl phongQl)
        {
            if (ModelState.IsValid)
            {
                db.PhongQls.Add(phongQl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phongQl);
        }
        [Route("NhaCC")]
        [HttpGet]
        public IActionResult AddNhaCC()
        {
            return View();
        }
        [Route("NhaCC")]
        [HttpPost]
        public IActionResult AddNhaCC(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                db.NhaCungCaps.Add(nhaCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nhaCungCap);
        }

        [Route("HangHoa")]
        [HttpGet]
        public IActionResult AddHangHoa()
        {
            ViewBag.IdNhaCc = new SelectList(db.NhaCungCaps.OrderBy(x => x.Id), "Id", "TenNcc");
            return View();
        }
        [Route("HangHoa")]
        [HttpPost]
        public IActionResult AddHangHoa(HangHoa hang)
        {
            if (ModelState.IsValid)
            {
                db.HangHoas.Add(hang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hang);
        }
        [Route("HoaDonBan")]
        [HttpGet]
        public IActionResult AddHoaDonBan()
        {
            return View();
        }
        [Route("HoaDonBan")]
        [HttpPost]
        public IActionResult AddHoaDonBan(HoaDonBan hdb)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonBans.Add(hdb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hdb);
        }
        [Route("HoaDonNhap")]
        [HttpGet]
        public IActionResult AddHoaDonNhap()
        {
            return View();
        }

        [Route("HoaDonNhap")]
        [HttpPost]
        public IActionResult AddHoaDonNhap(HoaDonNhap hdn)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonNhaps.Add(hdn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hdn);
        }

        [Route("HoaDonXuat")]
        [HttpGet]
        public IActionResult AddHoaDonXuat()
        {
            return View();
        }

        [Route("HoaDonXuat")]
        [HttpPost]
        public IActionResult AddHoaDonXuat(HoaDonXuat hdx)
        {
            if (ModelState.IsValid)
            {
                db.HoaDonXuats.Add(hdx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hdx);
        }

        [Route("BanAn")]
        [HttpGet]
        public IActionResult AddBanAn()
        {
            return View();
        }

        [Route("BanAn")]
        [HttpPost]
        public IActionResult AddBanAn(BanAn ban)
        {
            if (ModelState.IsValid)
            {
                db.BanAns.Add(ban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ban);
        }

        [Route("Menu")]
        [HttpGet]
        public IActionResult AddMenu()
        {
            return View();
        }

        [Route("Menu")]
        [HttpPost]
        public IActionResult AddMenu(Menu mnu)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Add(mnu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mnu);
        }

        [Route("LoaiMenu")]
        [HttpGet]
        public IActionResult AddLoaiMenu()
        {
            return View();
        }

        [Route("LoaiMenu")]
        [HttpPost]
        public IActionResult AddLoaiMenu(LoaiMonAn loaimnu)
        {
            if (ModelState.IsValid)
            {
                db.LoaiMonAns.Add(loaimnu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaimnu);
        }
    }
}
