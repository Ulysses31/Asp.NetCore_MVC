using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models.ViewModels
{
	public class PiesListViewModel
	{
		public IEnumerable<Pie> Pies { get; set; }
		public string CurrentCategory { get; set; }
	}
}