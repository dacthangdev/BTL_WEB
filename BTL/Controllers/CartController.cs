using BTL.Models;
using BTL.Models.Authentication;
using BTL.ModelsView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace BTL.Controllers
{
	public class CartController : Controller
	{
		CsdlwebContext db = new CsdlwebContext();
		Boolean reload = false;
		public List<CartItem> ShoppingCarts
		{
			get
			{
				var gh = HttpContext.Session.Get<List<CartItem>>("GioHang");
				if (gh == null)
				{
					gh = new List<CartItem>();
				}
				return gh;
			}
		}
		[HttpPost]
		[Route("api/cart/add")]
		public bool AddToCart(int id, int? amount)
		{
			try
			{
				List<CartItem> carts = ShoppingCarts;
				CartItem item = carts.SingleOrDefault(p => p.menu.Id == id);
				if (item != null)
				{
					carts[carts.IndexOf(item)].amount ++;
				}
				else
				{
					Menu monan = db.Menus.SingleOrDefault(x => x.Id == id);
					item = new CartItem
					{
						amount = amount.HasValue ? amount.Value : 1,
						menu = monan
					};
					carts.Add(item);
				}
				// Lưu lại session
				HttpContext.Session.Set<List<CartItem>>("GioHang", carts);
				return true;
			}
			catch { return false; }
			//return RedirectToAction("Menu", "Home");
		}
		[HttpPost]
		[Route("api/cart/update")]
		public IActionResult UpdateCart(int id, int? amount)
		{
			List<CartItem> cart = ShoppingCarts;
			try
			{
				if (cart != null)
				{
					foreach(CartItem item in cart)
					{
						if (item.menu.Id == id && amount.HasValue)
						{
							item.amount = amount.Value;
							// Lưu lại session
							HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
							return RedirectToAction("Index", "Cart");
						}
					}
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index", "Cart");
			}
			return RedirectToAction("Index", "Cart");
		}
		[HttpPost]
		[Route("api/cart/remove")]
		public IActionResult Remove(int id)
		{
			try
			{
				List<CartItem> cart = ShoppingCarts;
				if (cart != null)
				{
					foreach (CartItem item in cart)
					{
						if (item.menu.Id == id)
						{
							cart.Remove(item);
							HttpContext.Session.Set<List<CartItem>>("GioHang", cart);
							return RedirectToAction("Index", "Cart");
						}
					}
				}
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index", "Cart");
			}
			return RedirectToAction("Index", "Cart");
		}
		 public IActionResult Index()
        {
			List<int> lstMenuids=  new List<int>();
			var lsGiohang = ShoppingCarts;
			if (lsGiohang.Count() > 0)
			{
				return View(lsGiohang);
			}
			else
			{
				return RedirectToAction("Menu", "Home");
			}

        }

		[HttpGet]
		public IActionResult CheckOut()
		{
			KhachHang customer = new KhachHang();
			var user = HttpContext.Session.Get<Tuser>("UserName");
            if (HttpContext.Session.Get<Tuser>("UserName") != null)
			{
				user = HttpContext.Session.Get<Tuser>("UserName");

			}
			else if (HttpContext.Session.Get<Tuser>("Manager") != null)
			{
                user = HttpContext.Session.Get<Tuser>("Manager");
            }
			if (user != null)
			{
                customer = db.KhachHangs.Where(x => x.Email.Equals(user.Email)).FirstOrDefault();
                ViewBag.Customer = customer;
			}
			if(ShoppingCarts.Count() >0)
			{
				return View(ShoppingCarts);
			}
			return View();
		}
        [Route("api/cart/checkout")]
        [HttpPost]
        public IActionResult ConfirmCheckOut(string TenKhachHang ,string SDT, string Email, string Diachi)
        {
			KhachHang khachHang = db.KhachHangs.Where(x=> x.HoTen == TenKhachHang && x.Sdt == SDT && x.Email == Email).FirstOrDefault();
			if (khachHang == null)
			{
                khachHang = new KhachHang {
					Email = Email,
					HoTen = TenKhachHang,
					Sdt = SDT,
					Img = null
				};
			}
            db.Update(khachHang);
            db.SaveChanges();
            try
			{
                List<CartItem> carts = ShoppingCarts;
                HoaDonBan hoaDonBan = new HoaDonBan();
                hoaDonBan.IdKh = khachHang.Id;
                hoaDonBan.NgayXuat = DateTime.Now;
                hoaDonBan.SoHd = "Đơn Đặt Hàng của " + khachHang.HoTen + " " + Diachi.ToString();
				hoaDonBan.TongTien =(float) carts.Sum(x => x.TotalMoney);
                db.Add(hoaDonBan);
                db.SaveChanges();
                foreach (CartItem item in carts)
                {
                    ChitietHdb chitietHdb = new ChitietHdb();
                    chitietHdb.IdHdb = hoaDonBan.Id;
                    chitietHdb.IdMenu = item.menu.Id;
                    chitietHdb.Sl = item.amount;
                    chitietHdb.GiaTien = item.TotalMoney;
                    db.Add(chitietHdb);
                }
                db.SaveChanges();
                // Clear giỏ hàng 
                HttpContext.Session.Remove("GioHang");
                return RedirectToAction("Index", routeValues: "Home");
            }
			catch (Exception ex)
			{
                ViewBag.Customer = khachHang;
                return View(ShoppingCarts);
			}
            return View();
        }
		[Route("api/cart/details")]
		[HttpGet]
		public List<OrderDetail> getDetailCTHDB(int id)
		{
            List<ChitietHdb> chitietHdbs = db.ChitietHdbs.Where(x => x.IdHdb == id).ToList();
			List<OrderDetail> orderDetails = new List<OrderDetail>();
			foreach(var item in chitietHdbs)
			{
				Menu menu = db.Menus.Where(x=>x.Id == item.IdMenu).FirstOrDefault();
				OrderDetail orderDetail = new OrderDetail
				{
					Sl = item.Sl,
					GiaTien = item.GiaTien,
					menu = menu.TenMon
				};
				orderDetails.Add(orderDetail);
			}
            return orderDetails;
        }
    }
}
