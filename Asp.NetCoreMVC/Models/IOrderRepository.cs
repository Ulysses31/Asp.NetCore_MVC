using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models
{
	public interface IOrderRepository
	{
		public IEnumerable<Order> GetAllOrders(string userId);

		public Order GetOrderById(int id);

		public void CreateOrder(Order order);
	}
}