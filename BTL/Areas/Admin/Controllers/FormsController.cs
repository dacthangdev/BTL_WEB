using BTL.Models;
using BTL.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Forms")]
    [Route("Tables")]
    [Authentication]
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
            if (formFile != null)
            {
                Guid guid = Guid.NewGuid();

                string newfileName = guid.ToString();

                string fileextention = Path.GetExtension(formFile.FileName);

                string fileName = newfileName + fileextention;

                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Admin\\Images", fileName);

                var stream = new FileStream(uploadpath, FileMode.Create);

                formFile.CopyToAsync(stream);
                nv.AnhDaiDien = fileName.ToString();
            }
            try
            {
                db.NhanViens.Add(nv);
                db.SaveChanges();
                return RedirectToAction("TableNhanVien", "Tables");
            }
            catch (Exception ex)
            {
                return View(nv);
            }
        }
        [Route("EditNhanVien")]
        [HttpGet]
        public IActionResult EditNhanVien(int IdNV)
        {
            ViewBag.IdPhong = new SelectList(db.PhongQls.OrderBy(x => x.Id), "Id", "TenPhong");
            ViewBag.IdCv = new SelectList(db.ChucVus.OrderBy(x => x.Id), "Id", "TenCv");
            var nhanVien = db.NhanViens.Find(IdNV);
            return View(nhanVien);
        }
        [Route("EditNhanVien")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditNhanVien(NhanVien nv, IFormFile formFile)
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
                nv.AnhDaiDien = fileName.ToString();
            }
            try
            {
                nv.IdCvNavigation = db.ChucVus.Find(nv.IdCv);
                nv.IdPhongNavigation = db.PhongQls.Find(nv.IdPhong);
                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TableNhanVien", "Tables");
            }
            catch (Exception ex)
            {
                return View(nv);
            }
        }

        [Route("DeleteNhanVien")]
        [HttpGet]
        public IActionResult DeleteNhanVien(int IdNV)
        {
            TempData["Message"] = "";
            var hoaDonBan = db.HoaDonBans.Where(x => x.IdNv == IdNV).ToList();
            var hoaDonNhap = db.HoaDonNhaps.Where(x => x.IdNv == IdNV).ToList();
            var hoaDonXuat = db.HoaDonXuats.Where(x => x.IdNv == IdNV).ToList();
            if (hoaDonBan.Count() > 0 || hoaDonNhap.Count() > 0 || hoaDonXuat.Count() > 0)
            {
                TempData["Message"] = "Không xoá được nhân viên này!";
                return RedirectToAction("TableNhanVien", "Tables");
            }
            db.Remove(db.NhanViens.Find(IdNV));
            db.SaveChanges();
            TempData["Message"] = "Nhân viên này đã được xoá!";
            return RedirectToAction("TableNhanVien", "Tables");

        }
        [Route("KhachHang")]
        [HttpGet]
        public IActionResult AddKhachHang()
        {
            return View();
        }
        [Route("KhachHang")]
        [HttpPost]
        public IActionResult AddKhachHang(KhachHang khachHang, IFormFile formFile)
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
                khachHang.Img = fileName.ToString();
            }
            else
            {
                khachHang.Img = null;
            }
            try
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("TableKhachHang", "Tables");
            }
            catch(Exception ex)
            {
                return View(khachHang);
            }
        }
        [Route("EditKhachHang")]
        [HttpGet]
        public IActionResult EditKhachHang(int IdKH)
        {
            var khachHang = db.KhachHangs.Find(IdKH);
            return View(khachHang);
        }
        [Route("EditKhachHang")]
        [HttpPost]
        public IActionResult EditKhachHang(KhachHang khachHang, IFormFile formFile)
        {
            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages\\anhWeb", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            khachHang.Img = fileName.ToString();
            if (ModelState.IsValid)
            {
                db.KhachHangs.Update(khachHang);
                db.SaveChanges();
                return RedirectToAction("TableKhachHang", "Tables");
            }
            return View(khachHang);
        }


        [Route("DeleteKhachHang")]
        [HttpGet]
        public IActionResult DeleteKhachHang(int IdKH)
        {
            TempData["Message"] = "";
            var hoaDonBan = db.HoaDonBans.Where(x => x.IdKh == IdKH).ToList();

            if (hoaDonBan.Count() > 0)
            {
                TempData["Message"] = "Không xoá được khách hàng này!";
                return RedirectToAction("TableKhachHang", "Tables");
            }
            db.Remove(db.KhachHangs.Find(IdKH));
            db.SaveChanges();
            TempData["Message"] = "Khách hàng này đã được xoá!";
            return RedirectToAction("TableKhachHang", "Tables");

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
                return RedirectToAction("TablePhongBan", "Tables");
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
                hang.Sl = 0;
                db.HangHoas.Add(hang);
                db.SaveChanges();
                return RedirectToAction("TableHangHoa","Tables");
            }
            catch (Exception ex)
            {
                return View(hang);
            }
            
        }

        [Route("EditHangHoa")]
        [HttpGet]
        public IActionResult EditHangHoa(int ID)
        {
            ViewBag.IdNhaCc = new SelectList(db.NhaCungCaps.OrderBy(x => x.Id), "Id", "TenNcc");
            HangHoa hangHoa = db.HangHoas.Find(ID);
            return View(hangHoa);
        }
        [Route("EditHangHoa")]
        [HttpPost]
        public IActionResult EditHangHoa(HangHoa hang)
        {
            try
            {
                db.Update(hang);
                db.SaveChanges();
                return RedirectToAction("TableHangHoa", "Tables");
            }
            catch (Exception ex)
            {
                return View(hang);
            }

        }

        [Route("DeleteHangHoa")]
        [HttpGet]
        public IActionResult DeleteHangHoa(int ID)
        {
            TempData["Message"] = "";
            var hoaDonNhap = db.ChitietHdns.Where(x => x.IdHh == ID).ToList();
            var hoaDonXuat = db.ChitietHdxes.Where(x => x.IdHh == ID).ToList();

            if (hoaDonNhap.Count() >  0 || hoaDonXuat.Count() > 0)
            {
                TempData["Message"] = "Không xoá được hàng hóa này!";
                return RedirectToAction("TableHangHoa", "Tables");
            }
            db.Remove(db.HangHoas.Find(ID));
            db.SaveChanges();
            TempData["Message"] = "Hàng này đã được xoá!";
            return RedirectToAction("TableHangHoa", "Tables");
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
                return RedirectToAction("TableHoaDonBan","Tables");
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
                return RedirectToAction("TableHoaDonXuat", "Tables");
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
                return RedirectToAction("TableHoaDon");
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

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ProductImages\\anhWeb", fileName);

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
            hdb.GiaTien = (int)giatien;
            hdb.KhuyenMai = 0;
            try
            {
                ChitietHdb chitietHdb = db.ChitietHdbs.Where(x => x.IdMenu == hdb.IdMenu && x.IdHdb == hdb.IdHdb).FirstOrDefault();
                if (chitietHdb != null)
                {
                    chitietHdb.Sl += hdb.Sl;
                    chitietHdb.GiaTien += hdb.GiaTien;
                    db.ChitietHdbs.Update(chitietHdb);
                    db.SaveChanges();
                }
                else
                {
                    db.ChitietHdbs.Add(hdb);
					db.SaveChanges();
                }
                HoaDonBan hoadon = db.HoaDonBans.Find(hdb.IdHdb);
                List<ChitietHdb> chitietHdbs = db.ChitietHdbs.Where(x => x.IdHdb == hdb.IdHdb).ToList();
                hoadon.TongTien = chitietHdbs.Sum(x => x.GiaTien);
                db.Update(hoadon);
                db.SaveChanges();
                return RedirectToAction("TableChiTietHDB", new { idHDB = hdb.IdHdb });
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
                    HoaDonNhap hoaDon = db.HoaDonNhaps.Where(x=>x.Id == hdn.IdHdn).FirstOrDefault();
                    hangHoa.Sl += hdn.Sl;
                    hangHoa.NgayNhap = hoaDon.NgayNhap;
                }
                db.Update(hangHoa);
                db.SaveChanges();
                return RedirectToAction("TableChiTietHDN", new { idHDN = hdn.IdHdn });
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
                return RedirectToAction("TableChiTietHDN", new { idHDX = hdx.IdHdx });
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
        [Route("Blog")]
        [HttpGet]
        public IActionResult AddBlog()
        {
            return View();
        }

        [Route("Blog")]
        [HttpPost]
        public IActionResult AddBlog(Blog loaiblg)
        {
            if (ModelState.IsValid)
            {
                db.Blogs.Add(loaiblg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiblg);
        }
        [Route("DeleteChiTietHDB")]
        [HttpGet]
        public IActionResult DeleteChiTietHDB(int IDHDB, int IDMENU)
        {
            TempData["Message"] = "";
            try
            {
                ChitietHdb chitiet = db.ChitietHdbs.Where(x => x.IdHdb == IDHDB && x.IdMenu == IDMENU).FirstOrDefault();
                db.Remove(chitiet);
                db.SaveChanges();
                HoaDonBan hoadon = db.HoaDonBans.Find(IDHDB);
                List<ChitietHdb> chitietHdbs = db.ChitietHdbs.Where(x => x.IdHdb == IDHDB).ToList();
                hoadon.TongTien = chitietHdbs.Sum(x => x.GiaTien);
                db.Update(hoadon);
                db.SaveChanges();
                if (chitietHdbs == null)
                {
                    return RedirectToAction("TableHoaDonBan", "Tables");
                }
                return RedirectToAction("TableChiTietHDB", "Tables", new { idHDB = IDHDB });
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Chi Tiết hóa đơn này không được xóa";
                return RedirectToAction("TableChiTietHDB", "Tables", new { idHDB = IDHDB });
            }
            return RedirectToAction("TableChiTietHDB", "Tables", new { idHDB = IDHDB });
        }
        [Route("DeleteChiTietHDN")]
        [HttpGet]
        public IActionResult DeleteChiTietHDN(int IDHDN, int IDHH)
        {
            TempData["Message"] = "";
            try
            {
                ChitietHdn chitiet = db.ChitietHdns.Where(x => x.IdHdn == IDHDN && x.IdHh == IDHH).FirstOrDefault();
                db.Remove(chitiet);
                db.SaveChanges();
                HangHoa hangHoa = db.HangHoas.Find(IDHH);
                List<ChitietHdn> chitietHdns = db.ChitietHdns.Where(x => x.IdHh == IDHH).ToList();
                List<ChitietHdx> chitietHdxs = db.ChitietHdxes.Where(x => x.IdHh == IDHH).ToList();
                hangHoa.Sl = chitietHdns.Sum(x => x.Sl) - chitietHdxs.Sum(x => x.Sl);
                db.Update(hangHoa);
                db.SaveChanges();
                if (chitietHdns == null)
                {
                    return RedirectToAction("TableHoaDonNhap", "Tables");
                }
                return RedirectToAction("TableChiTietHDN", "Tables", new { idHDN = IDHDN });
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Chi Tiết hóa đơn này không được xóa";
                return RedirectToAction("TableChiTietHDN", "Tables", new { idHDN = IDHDN });
            }
            return RedirectToAction("TableChiTietHDN", "Tables", new { idHDN = IDHDN });
        }
        [Route("DeleteChiTietHDX")]
        [HttpGet]
        public IActionResult DeleteChiTietHDX(int IDHDX, int IDHH)
        {
            TempData["Message"] = "";
            try
            {
                ChitietHdx chitiet = db.ChitietHdxes.Where(x => x.IdHdx == IDHDX && x.IdHh == IDHH).FirstOrDefault();
                db.Remove(chitiet);
                db.SaveChanges();
                HangHoa hangHoa = db.HangHoas.Find(IDHH);
                List<ChitietHdn> chitietHdns = db.ChitietHdns.Where(x => x.IdHh == IDHH).ToList();
                List<ChitietHdx> chitietHdxs = db.ChitietHdxes.Where(x => x.IdHh == IDHH).ToList();
                hangHoa.Sl = chitietHdns.Sum(x => x.Sl) - chitietHdxs.Sum(x => x.Sl);
                db.Update(hangHoa);
                db.SaveChanges();
                if (chitietHdxs == null)
                {
                    return RedirectToAction("TableHoaDonXuat", "Tables");
                }
                return RedirectToAction("TableChiTietHDX", "Tables", new { idHDX = IDHDX });
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Chi Tiết hóa đơn này không được xóa";
                return RedirectToAction("TableChiTietHDX", "Tables", new { idHDX = IDHDX });
            }
            return RedirectToAction("TableChiTietHDX", "Tables", new { idHDX = IDHDX });
        }

        [Route("EditUser")]
        [HttpGet]
        public IActionResult EditUser(string id)
        {
            Tuser user = db.Tusers.Find(id);
            return View(user);
        }

        [Route("EditUser")]
        [HttpPost]
        public IActionResult EditUser(Tuser user)
        {
            if (ModelState.IsValid)
            {
                db.Update(user);
                db.SaveChanges();
                return RedirectToAction("TableUser","Tables");
            }
            return View(user);
        }

        [Route("DeleteUser")]
        [HttpPost]
        public IActionResult DeleteUser(string id)
        {
            try
            {
                Tuser user = db.Tusers.Find(id);
                db.Remove(user);
                db.SaveChanges();
                return RedirectToAction("TableUser", "Tables");
            }
            catch (Exception e)
            {
                return RedirectToAction("TableUser", "Tables");
            }
           
        }

    }
}
