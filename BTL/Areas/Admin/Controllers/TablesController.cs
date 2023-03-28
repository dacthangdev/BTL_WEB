using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Tables")]
    public class TablesController : Controller
    {
        [Route("TableHoaDonBan")]
        public IActionResult TableHoaDonBan()
        {

            return View();
        }
    }
}
