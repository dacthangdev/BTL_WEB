using BTL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;


namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Forms")]
    [Route("Tables")]
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
        public IActionResult AddNhanVien(NhanVien nv, IFormFile formFile)
        {
            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Admin\\Images", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            try
            {
                nv.AnhDaiDien = fileName.ToString();
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("TableNhanVien");
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
            try
            {
                db.HangHoas.Add(hang);
                db.SaveChanges();
                return RedirectToAction("TableHangHoa");
            }
            catch (Exception ex)
            {
                return View(hang);
            }
            
        }

        [Route("HoaDonBan")]
        [HttpGet]
        public IActionResult AddHoaDonBan()
        {
            ViewBag.IdNv = new SelectList(db.NhanViens.OrderBy(x => x.Id), "Id", "HoTen");
            ViewBag.IdKh = new SelectList(db.KhachHangs.OrderBy(x => x.Id), "Id", "HoTen");
            ViewBag.IdBan = new SelectList(db.BanAns.OrderBy(x => x.Id), "Id", "Id");
            return View();
        }
        [Route("HoaDonBan")]
        [HttpPost]
        public IActionResult AddHoaDonBan(HoaDonBan hdb)
        {
            hdb.SoHd = "HoaDonBan_" + hdb.NgayXuat;
            try
            {
                db.HoaDonBans.Add(hdb);
                db.SaveChanges();
                return RedirectToAction("TableHoaDonBan");
            }
            catch (Exception e)
            {
                return View(hdb);
            }
            
        }
        [Route("HoaDonNhap")]
        [HttpGet]
        public IActionResult AddHoaDonNhap()
        {
            ViewBag.IdNv = new SelectList(db.NhanViens, "Id", "HoTen");
            return View();
        }

        [Route("HoaDonNhap")]
        [HttpPost]
        public IActionResult AddHoaDonNhap(HoaDonNhap hdn)
        {
            try
            {
                db.HoaDonNhaps.Add(hdn);
                db.SaveChanges();
                return RedirectToAction("TableHoaDonNhap");
            }
            catch(Exception e)
            {
                return View(hdn);
            }
        }

        [Route("HoaDonXuat")]
        [HttpGet]
        public IActionResult AddHoaDonXuat()
        {
            ViewBag.IdNv = new SelectList(db.NhanViens, "Id", "HoTen");
            return View();
        }

        [Route("HoaDonXuat")]
        [HttpPost]
        public IActionResult AddHoaDonXuat(HoaDonXuat hdx)
        {
            try
            {
                db.HoaDonXuats.Add(hdx);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(hdx);
            }
        }

        [Route("BanAn")]
        [HttpGet]
        public IActionResult AddBanAn()
        {
            ViewBag.IdTang = new SelectList(db.Tangs.OrderBy(x => x.Id), "Id", "TenTang"); ;
            return View();
        }

        [Route("BanAn")]
        [HttpPost]
        public IActionResult AddBanAn(BanAn ban)
        {
            ViewBag.IdTang = new SelectList(db.Tangs.OrderBy(x => x.Id), "Id", "TenTang"); ;
            try
            {
                db.BanAns.Add(ban);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View(ban);
            }
        }

        [Route("Menu")]
        [HttpGet]
        public IActionResult AddMenu()
        {
            ViewBag.IdLoai = new SelectList(db.LoaiMonAns, "Id", "TenLoai");
            return View();
        }

        [Route("Menu")]
        [HttpPost]
        public IActionResult AddMenu(Menu mnu, IFormFile formFile)
        {
            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Admin\\Images", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            mnu.Anh = fileName.ToString();
            try
            {
                db.Menus.Add(mnu);
                db.SaveChanges();
                return RedirectToAction("TableMenu");
            }
            catch(Exception e)
            {
                return View(mnu);
            }
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
        [Route("ChiTietHDB")]
        [HttpGet]
        public IActionResult AddChiTietHDB(int idHDB)
        {
            ViewBag.IdHdb = new SelectList(db.HoaDonBans.Where(x=>x.Id == idHDB),"Id", "SoHd");
            ViewBag.IdMenu = new SelectList(db.Menus.OrderBy(x => x.Id), "Id", "TenMon");
            return View();
        }
        [Route("ChiTietHDB")]
        [HttpPost]
        public IActionResult AddChiTietHDB(ChitietHdb hdb)
        {
            Menu? monan = db.Menus.Where(x => x.Id == hdb.IdMenu).FirstOrDefault();
            double giatien = hdb.Sl * monan.Gia;
            hdb.GiaTien = ((int)(giatien - giatien * hdb.KhuyenMai/100));
            if (hdb.KhuyenMai == null)
            {
                hdb.KhuyenMai = 0;
            }
            try
            {
                ChitietHdb chitietHdb = db.ChitietHdbs.Where(x => x.IdMenu == hdb.IdMenu && x.IdHdb == hdb.IdHdb).FirstOrDefault();
                if (chitietHdb != null)
                {
                    chitietHdb.Sl += hdb.Sl;
                    chitietHdb.GiaTien += hdb.GiaTien;
                    db.ChitietHdbs.Update(chitietHdb);
                    db.SaveChanges();
                    return RedirectToAction("TableChiTietHDB", routeValues: hdb.IdHdb);
                }
                else
                {
                    db.ChitietHdbs.Add(hdb);
                    db.SaveChanges();
                    return RedirectToAction("TableChiTietHDB", hdb.IdHdb);
                }
            }
            catch (Exception ex)
            {
                return View(hdb);
            }
        }

        [Route("ChiTietHDN")]
        [HttpGet]
        public IActionResult AddChiTietHDN(int idHDN)
        {
            ViewBag.IdHdn = new SelectList(db.HoaDonNhaps.Where(x => x.Id == idHDN), "Id", "NgayNhap");
            ViewBag.IdHh = new SelectList(db.HangHoas, "Id", "TenHh");
            return View();
        }
        [Route("ChiTietHDN")]
        [HttpPost]
        public IActionResult AddChiTietHDN(ChitietHdn hdn)
        {
            try
            {
                db.ChitietHdns.Add(hdn);
                HangHoa hangHoa = db.HangHoas.Where(x => x.Id == hdn.IdHh).FirstOrDefault();
                if (hangHoa != null)
                {
                    hangHoa.Sl += hdn.Sl;
                }
                db.Update(hangHoa);
                db.SaveChanges();
                return RedirectToAction("TableChiTietHDN");
            }
            catch (Exception e)
            {
                return View(hdn);
            }
        }

        [Route("ChiTietHDX")]
        [HttpGet]
        public IActionResult AddChiTietHDX(int idHDX)
        {
            ViewBag.IdHdx = new SelectList(db.HoaDonXuats.Where(x => x.Id == idHDX), "Id", "NgayXuat");
            ViewBag.IdHh = new SelectList(db.HangHoas, "Id", "TenHh");
            return View();
        }
        [Route("ChiTietHDX")]
        [HttpPost]
        public IActionResult AddChiTietHDX(ChitietHdx hdx)
        {
            try
            {
                db.ChitietHdxes.Add(hdx);
                HangHoa hangHoa = db.HangHoas.Where(x => x.Id == hdx.IdHh).FirstOrDefault();
                if (hangHoa != null)
                {
                    hangHoa.Sl -= hdx.Sl;
                }
                db.Update(hangHoa);
                db.SaveChanges();
                return RedirectToAction("TableChiTietHDN");
            }
            catch (Exception e)
            {
                return View(hdx);
            }
        }

        [Route("Tang")]
        [HttpGet]
        public IActionResult AddTang()
        {
            return View();
        }

        [Route("Tang")]
        [HttpPost]
        public IActionResult AddTang(Tang tang)
        {
            if (ModelState.IsValid)
            {
                db.Tangs.Add(tang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tang);
        }
    }
}
