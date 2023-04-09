using BTL.Models;

namespace BTL.ModelsView
{
	public class CartItem
	{
		public Menu menu { get; set; }
		public int amount { get; set; }
		public int TotalMoney => amount* menu.Gia;
	}
}
