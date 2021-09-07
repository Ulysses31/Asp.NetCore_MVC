using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCoreMVC.Models
{
	public class PieReviewRepository : IPieReviewRepository
	{
		private readonly AppDbContext _appDbContext;

		public PieReviewRepository(AppDbContext appDbContext)
		{
			this._appDbContext = appDbContext;
		}

		public void AddPieReview(PieReview pieReview)
		{
			_appDbContext.PieReviews.Add(pieReview);
			_appDbContext.SaveChanges();
		}

		public IEnumerable<PieReview> GetPieReviews(int pieId)
		{
			return _appDbContext.PieReviews.Where(p => p.Pie.PieId == pieId);
		}
	}
}