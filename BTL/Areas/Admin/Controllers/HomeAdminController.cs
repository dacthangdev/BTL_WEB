using BTL.Models.Authentication;
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
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
