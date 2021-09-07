using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMVC.Models.ViewModels
{
	public class PieDetailViewModel
	{
		public Pie Pie { get; set; }

		public string Review { get; set; }
	}
}