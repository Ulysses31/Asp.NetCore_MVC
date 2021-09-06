using Asp.NetCoreMVC.Models;

namespace Asp.NetCoreMVC.Models.ViewModels
{
	public class ShoppingCartViewModel
	{
		public ShoppingCart ShoppingCart { get; set; }
		public decimal ShoppingCartTotal { get; set; }
	}
}