using BTL.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin")]
    [Route("Tables")]
    public class TablesController : Controller
    {
        CsdlwebContext db = new CsdlwebContext();
        [Route("TableHoaDonBan")]
        public IActionResult TableHoaDonBan()
        {
            List<HoaDonBan> list = db.HoaDonBans.ToList();
            return View(list);
        }
    }
}
