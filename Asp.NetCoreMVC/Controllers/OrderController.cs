using Asp.NetCoreMVC.Models;
using Asp.NetCoreMVC.Models.ViewModels;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Asp.NetCoreMVC.Controllers
{
	[Authorize]
	public class OrderController : Controller
	{
		private readonly IHtmlHelper _htmlHelper;
		private readonly INotyfService _notyf;
		private readonly IOrderRepository _orderRepository;
		private readonly ShoppingCart _shoppingCart;

		public IEnumerable<SelectListItem> Shippers { get; set; }

		public OrderController(
			IHtmlHelper htmlHelper,
			INotyfService notyf,
			IOrderRepository orderRepository,
			ShoppingCart shoppingCart
		)
		{
			this._htmlHelper = htmlHelper;
			this._notyf = notyf;
			this._orderRepository = orderRepository;
			this._shoppingCart = shoppingCart;
		}

		public ViewResult List()
		{
			var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

			IEnumerable<Order> orders = _orderRepository
																		.GetAllOrders(userId)
																		.OrderByDescending(o => o.OrderPlaced);

			return View(orders);
		}

		public IActionResult Checkout()
		{
			Shippers = _htmlHelper.GetEnumSelectList<ShipperType>();

			CheckoutViewModel checkoutViewModel = new CheckoutViewModel {
				Order = new Order(),
				Shippers = Shippers
			};

			return View(checkoutViewModel);
		}

		[HttpPost]
		[RequireHttps]
		[ValidateAntiForgeryToken]
		public IActionResult Checkout(Order order)
		{
			if(!Request.IsHttps) {
				return new StatusCodeResult(StatusCodes.Status403Forbidden);
			}

			var items = _shoppingCart.GetShoppingCartItems();
			_shoppingCart.ShoppingCartItems = items;

			if (_shoppingCart.ShoppingCartItems.Count == 0) {
				ModelState.AddModelError("", "Your cart is empty.");
			}

			if (ModelState.IsValid) {
				_orderRepository.CreateOrder(order);
				_shoppingCart.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}

			return View(order);
		}

		public IActionResult CheckoutComplete()
		{
			ViewBag.Message = "Thanks for your order.";
			_notyf.Success("Order successfully completed!");
			return View();
		}
	}
}