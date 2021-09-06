using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Asp.NetCoreMVC.Models
{
	public class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _appDbContext;
		private readonly ShoppingCart _shoppingCart;
		private readonly IServiceProvider _serviceProvider;

		public OrderRepository(
			AppDbContext appDbContext,
			ShoppingCart shoppingCart,
			IServiceProvider serviceProvider
		)
		{
			this._appDbContext = appDbContext;
			this._shoppingCart = shoppingCart;
			this._serviceProvider = serviceProvider;
		}

		public IEnumerable<Order> GetAllOrders(string userId)
		{
			if (!string.IsNullOrEmpty(userId)) {
				return _appDbContext.Orders
					.Where(o => o.UserId == userId)
					.Include(o => o.OrderDetails)
					.ThenInclude(o => o.Pie)
					.Include(o => o.User)
					.ToList();
			}
			else {
				return _appDbContext.Orders
					.Include(o => o.OrderDetails)
					.ThenInclude(o => o.Pie)
					.Include(o => o.User)
					.ToList();
			}
		}

		public Order GetOrderById(int id)
		{
			return _appDbContext.Orders
				.Where(o => o.OrderId == id)
				.Include(o => o.OrderDetails)
				.Include(o => o.User)
				.FirstOrDefault();
		}

		public void CreateOrder(Order order)
		{
			var userService = _serviceProvider.GetRequiredService<IHttpContextAccessor>();
			var userId = userService.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			order.UserId = userId;
			order.OrderPlaced = DateTime.Now;
			order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
			order.OrderDetails = new List<OrderDetail>();

			var shoppingCartItems = _shoppingCart.ShoppingCartItems;

			foreach (var shoppingCartItem in shoppingCartItems) {
				var orderDetail = new OrderDetail() {
					Amount = shoppingCartItem.Amount,
					PieId = shoppingCartItem.Pie.PieId,
					Price = shoppingCartItem.Pie.Price
				};
				order.OrderDetails.Add(orderDetail);
			}

			_appDbContext.Orders.Add(order);
			_appDbContext.SaveChanges();
		}
	}
}