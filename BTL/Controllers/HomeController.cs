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
        static string anhblog;
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
            var lstblog = db.Blogs.AsNoTracking().Include(x=>x.CommentBlogs).OrderBy(x => x.NgayDang).Reverse();
            PagedList<Blog> lst = new PagedList<Blog>(lstblog, pageNumber, pageSize);

            return View(lst);
        }
        public IActionResult ViewBlog(int? id)
        {
            var lst = db.Blogs.Where(x => x.Id == id).ToList();
            List<CommentBlog> commentBlogs = db.CommentBlogs.Include(x=>x.IdUserNavigation).Where(x => x.IdBlog == id).ToList();
            List<Comments> comments = new List<Comments>();
            foreach (var item in commentBlogs)
            {
                Comments comment = new Comments
                {
                    User = item.IdUserNavigation.HoTen,
                    Content = item.Content,
                };
                comments.Add(comment);
            }
            if (comments.Count() > 0)
            {
                ViewBag.Comments = comments;
            }
            if (HttpContext.Session.Get<Tuser>("UserName") != null)
            {
                ViewBag.User = HttpContext.Session.Get<Tuser>("UserName");
            }
            else if (HttpContext.Session.Get<Tuser>("Manager") != null)
            {
                ViewBag.User = HttpContext.Session.Get<Tuser>("Manager");
            }
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
                List<HoaDonBan> hoaDonBans = db.HoaDonBans.Where(x => x.IdKh == khachHang.Id).ToList();
                ViewBag.ListHD = hoaDonBans;
            }
            return View(tuser);
        }
        [HttpGet]
        [Route("api/session")]
        public bool GetSessionData()
        {
            if (HttpContext.Session.Get<Tuser>("UserName") != null)
            {
               
                return true;
            }
            else if (HttpContext.Session.Get<Tuser>("Manager") != null)
            {
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("api/postblog")]
        public bool PostBlog(IFormFile formFile, string content, string title)
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
            Blog blog = new Blog
            {
                NgayDang = DateTime.Now,
                NoiDung = content,
                TacGia = tuser.HoTen,
                TieuDe = title
            };
            Guid guid = Guid.NewGuid();

            string newfileName = guid.ToString();

            string fileextention = Path.GetExtension(formFile.FileName);

            string fileName = newfileName + fileextention;

            string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Blog", fileName);

            var stream = new FileStream(uploadpath, FileMode.Create);

            formFile.CopyToAsync(stream);
            blog.Anh = fileName.ToString();
            try
            {
                db.Add(blog);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        [HttpDelete]
        [Route("api/deleteblog")]
        public bool DeleteBlog(int ID)
        {
            try
            {
                List<CommentBlog> comments = db.CommentBlogs.Where(x => x.IdBlog == ID).ToList();
                foreach (CommentBlog comment in comments)
                {
                    db.Remove(comment);
                }
                db.SaveChanges();
                db.Remove(db.Blogs.Find(ID));
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet]
        [Route("api/editblog")]
        public Blog editblog(int ID)
        {
            try
            {
                anhblog = db.Blogs.Find(ID).Anh;
                return db.Blogs.Find(ID);
            }
            catch (Exception ex)
            {
                return new Blog();
            }
        }

        [HttpPost]
        [Route("api/editblog")]
        public bool editblog(int ID, IFormFile formFile, string content, string title)
        {
            Blog blog = db.Blogs.Find(ID);
            blog.NgayDang = DateTime.Now;
            blog.NoiDung = content;
            blog.TieuDe = title;
            if (formFile != null)
            {
                Guid guid = Guid.NewGuid();

                string newfileName = guid.ToString();

                string fileextention = Path.GetExtension(formFile.FileName);

                string fileName = newfileName + fileextention;

                string uploadpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Blog", fileName);

                var stream = new FileStream(uploadpath, FileMode.Create);

                formFile.CopyToAsync(stream);

                blog.Anh = fileName.ToString();
            }
            else { blog.Anh = anhblog; }
            db.Update(blog);
            db.SaveChanges();
            return true;
            //try
            //{
            //    db.Update(blog);
            //    db.SaveChanges();
            //    return true;
            //}
            //catch (Exception e)
            //{
            //    return false;
            //}
        }

        [HttpGet]
        [Route("api/post/comments")]
        public List<Comments> PostComment(int ID_Blog, string Content)
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
            CommentBlog commentBlog = new CommentBlog
            {
                IdBlog = ID_Blog,
                IdUser = tuser.Username,
                Content = Content
            };
            db.Add(commentBlog);
            db.SaveChanges();
            List<CommentBlog> commentBlogs = db.CommentBlogs.Include(x => x.IdUserNavigation).Where(x => x.IdBlog == ID_Blog).ToList();
            List<Comments> comments = new List<Comments>();
            foreach (var item in commentBlogs)
            {
                Comments comment = new Comments
                {
                    User = item.IdUserNavigation.HoTen,
                    Content = item.Content,
                };
                comments.Add(comment);
            }
            return comments;
        }
        [HttpPost]
        [Route("api/changePassword")]
        public bool changePassword(string old_pw,string new_pw, string check_new_pw)
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
            if (!tuser.Password.Trim().Equals(old_pw))
            {
                return false;
            } 
            if (!new_pw.Equals(check_new_pw))
            {
                return false;
            }
            tuser.Password = new_pw;
            db.Update(tuser);
            db.SaveChanges();
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("Mangager");
            return true;
        }

    }
}