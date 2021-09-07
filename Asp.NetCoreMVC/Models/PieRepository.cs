using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreMVC.Models
{
	public class PieRepository : IPieRepository
	{
		private readonly AppDbContext _dbContext;

		public PieRepository(AppDbContext dbContext)
		{
			this._dbContext = dbContext;
		}

		public IEnumerable<Pie> GetAllPies()
		{
			return _dbContext.Pies.Include(c => c.Category);
		}

		public Pie GetPieById(int pieId)
		{
			return _dbContext.Pies
				.Include(c => c.Category)
				.Include(c => c.PieReviews)
				.ThenInclude(c => c.User)
				.FirstOrDefault(p => p.PieId == pieId);
		}

		public IEnumerable<Pie> PiesOfTheWeek()
		{
			return _dbContext.Pies
				.Include(c => c.Category)
				.Where(p => p.IsPieOfTheWeek);
		}
	}
}