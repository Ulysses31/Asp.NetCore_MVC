using System.Collections.Generic;

namespace Asp.NetCoreMVC.Models
{
	public interface IPieReviewRepository
	{
		public void AddPieReview(PieReview pieReview);

		public IEnumerable<PieReview> GetPieReviews(int pieId);
	}
}