using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models
{
	public interface IPieRepository
	{
		public IEnumerable<Pie> GetAllPies();

		public IEnumerable<Pie> PiesOfTheWeek();

		public Pie GetPieById(int pieId);
	}
}