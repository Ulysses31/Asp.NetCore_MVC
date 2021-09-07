using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("PieReview")]
	public class PieReview
	{
		[Key]
		[Column("PieReviewId")]
		public int PieReviewId { get; set; }

		public Pie Pie { get; set; }

		[ForeignKey("User")]
		[Column("UserId", TypeName = "nvarchar(450)")]
		public string UserId { get; set; }

		public IdentityUser User { get; set; }

		[Column("Review", TypeName = "nvarchar(255)")]
		[Required]
		[StringLength(255)]
		public string Review { get; set; }
	}
}