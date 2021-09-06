using Asp.NetCoreMVC.Models;
using Asp.NetCoreMVC.Models.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCoreMVC.Controllers
{
	public class ShoppingCartController : Controller
	{
		private readonly INotyfService _notyf;
		private readonly IPieRepository _pieRepository;
		private readonly ShoppingCart _shoppingCart;

		public ShoppingCartController(
			INotyfService notyf,
			IPieRepository pieRepository,
			ShoppingCart shoppingCart
		)
		{
			this._notyf = notyf;
			this._pieRepository = pieRepository;
			this._shoppingCart = shoppingCart;
		}

		public ViewResult Index()
		{
			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			var shoppingCartViewModel = new ShoppingCartViewModel() {
				ShoppingCart = _shoppingCart,
				ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
			};

			return View(shoppingCartViewModel);
		}

		public RedirectToActionResult AddToShoppingCart(int pieId)
		{
			var selectedPie = _pieRepository.GetPieById(pieId);

			if (selectedPie != null) {
				_shoppingCart.AddToCart(selectedPie, 1);
				_notyf.Success("Pie added to the cart!");
			}

			return RedirectToAction("Index");
		}

		public RedirectToActionResult RemoveFromShoppingCart(int pieId)
		{
			var selectedPie = _pieRepository.GetPieById(pieId);

			if (selectedPie != null) {
				_shoppingCart.RemoveFromCart(selectedPie);
				_notyf.Success("Pie removed from the cart!");
			}

			return RedirectToAction("Index");
		}
	}
}