using Microsoft.AspNetCore.Mvc;

namespace BTL.Areas.Admin.Controllers
{
	public class HomeAdminController : Controller
	{
		[Area("Admin")]
		[Route("Admin/HomeAdmin")]
		[Route("HomeAdmin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
