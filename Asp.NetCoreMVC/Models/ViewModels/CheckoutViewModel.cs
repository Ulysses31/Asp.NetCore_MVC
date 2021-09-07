using Asp.NetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models.ViewModels
{
	public class CheckoutViewModel
	{
		public IEnumerable<SelectListItem> Shippers { get; set; }
		public Order Order { get; set; }
	}
}