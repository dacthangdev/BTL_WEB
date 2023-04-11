using BTL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using BTL.Models.RegisterViewModel;
using Azure;
using System.Reflection.Metadata;
using BTL.ModelsView;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using BTL.Models.Authentication;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        CsdlwebContext db= new CsdlwebContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstsanpham = db.Menus.AsNoTracking().OrderBy(x => x.TenMon).Take(8).ToList();
            ViewBag.MonAn = db.Menus.AsNoTracking().OrderBy(x => x.TenMon).Take(4).ToList();
            ViewBag.NV = db.NhanViens.AsNoTracking().OrderBy(x => x.HoTen).Take(4);
            ViewBag.KH = db.KhachHangs.Take(4);
            ViewBag.Blog = db.Blogs.Take(3);
            PagedList<NhanVien> pageList = new PagedList<NhanVien>(ViewBag.NV, pageNumber, 4);
            return View(lstsanpham);
        }

        public IActionResult About(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;
            var lstNhanVien = db.NhanViens.AsNoTracking().OrderBy(x => x.HoTen);
            PagedList<NhanVien> pageList = new PagedList<NhanVien>(lstNhanVien, pageNumber, pageSize);
            ViewBag.KhachHang = db.KhachHangs;
            return View(pageList);
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Menu(int? page)
        {
            var lstsanpham = db.Menus.Where(x=>x.IdLoai == 1).ToList();
            return View(lstsanpham);
        }
        public IActionResult Blog(int? page)
        {
            int pageSize = 6;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstblog = db.Blogs.AsNoTracking().OrderBy(x => x.Id);
            PagedList<Blog> lst = new PagedList<Blog>(lstblog, pageNumber, pageSize);

            return View(lst);
        }
        public IActionResult ViewBlog(int? id)
        {
            var lst = db.Blogs.Where(x => x.Id == id).ToList();
            return View(lst);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Reservation() 
        {
            return View();
                
        }

        public IActionResult Cart()
        {
            return View();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.Get<Tuser>("UserName") == null && HttpContext.Session.Get<Tuser>("Manager") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Profile", "Home");

            }
        }

        [HttpPost]
        public IActionResult Login(Tuser user)
        {
            if (HttpContext.Session.Get<Tuser>("UserName") != null || HttpContext.Session.Get<Tuser>("Manager") != null)
            {
                return View(user);
            }
            else
            {
                Tuser u = db.Tusers.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefault();
                if (u != null)
                {
                    if (u.LoaiUser == 0)
                    {
                        HttpContext.Session.Set<Tuser>("Manager", u);
                        HttpContext.Session.SetString("ManagerLogin", u.Email.ToString());
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        HttpContext.Session.Set<Tuser>("UserName", u);
                        HttpContext.Session.SetString("UserNameLogin", u.Email.ToString());
                        return RedirectToAction("Profile", "Home");
                    }
                }
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Mangager");
            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Tuser
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    LoaiUser = 1,
                    HoTen = model.HoTen
                    //Thêm các trường thông tin khác của người dùng vào đây
                };

                //Lưu thông tin đăng kí vào cơ sở dữ liệu
                db.Tusers.Add(user);
                await db.SaveChangesAsync();

                //Đăng nhập người dùng và chuyển hướng đến trang chính
                TempData["SuccessMessage"] = "Đăng kí thành công!";
                return RedirectToAction("Login");
            }

            //Hiển thị thông báo lỗi nếu đăng kí không thành công
            ModelState.AddModelError("", "Đăng kí không thành công.");
            return View(model);
        }
        public IActionResult Register()
        {
            return View();
        }
        [AuthenticationProfile]
        public IActionResult Profile()
        {
            Tuser tuser = new Tuser();
            if (HttpContext.Session.Get<Tuser>("UserName") != null)
            {
                tuser = HttpContext.Session.Get<Tuser>("UserName");
            }
            else if (HttpContext.Session.Get<Tuser>("Manager") != null)
            {
                tuser = HttpContext.Session.Get<Tuser>("Manager");
            }
            KhachHang khachHang = db.KhachHangs.Where(x => x.Email == tuser.Email).FirstOrDefault();
            if (khachHang != null)
            {
                ViewBag.Profile = khachHang;
            }
            return View(tuser);
        }
    }
}