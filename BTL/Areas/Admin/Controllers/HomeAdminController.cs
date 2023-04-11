using BTL.Models;
using BTL.Models.Authentication;
using BTL.ModelsView;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Forms")]
    [Route("Tables")]
    [Authentication]
    public class HomeAdminController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("Profile")]
        public IActionResult Profile()
        {
            Tuser tuser = HttpContext.Session.Get<Tuser>("Manager");
            KhachHang khachHang = db.KhachHangs.Where(x=>x.Email ==  tuser.Email).FirstOrDefault();
            if (khachHang != null)
            {
                ViewBag.Profile = khachHang;
            }
            return View(tuser);
        }
    }
}
