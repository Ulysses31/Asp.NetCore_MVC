using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("Pie")]
	public class Pie
	{
		[Key]
		[Column("PieId")]
		public int PieId { get; set; }

		[Column("Name", TypeName = "nvarchar(200)")]
		[MaxLength(200)]
		[Required()]
		public string Name { get; set; }

		[Column("ShortDescription", TypeName = "nvarchar(200)")]
		[MaxLength(200)]
		[Required()]
		public string ShortDescription { get; set; }

		[Column("LongDescription", TypeName = "nvarchar(max)")]
		[Required()]
		public string LongDescription { get; set; }

		[Column("AllergyInformation", TypeName = "nvarchar(255)")]
		[MaxLength(255)]
		public string AllergyInformation { get; set; }

		[Column("Price", TypeName = "decimal(18, 2)")]
		[Required()]
		public decimal Price { get; set; }

		[Column("ImageUrl", TypeName = "nvarchar(max)")]
		[Required()]
		public string ImageUrl { get; set; }

		[Column("ImageThumbnailUrl", TypeName = "nvarchar(max)")]
		[Required()]
		public string ImageThumbnailUrl { get; set; }

		[Column("IsPieOfTheWeek", TypeName = "bit")]
		public bool IsPieOfTheWeek { get; set; }

		[Column("InStock", TypeName = "bit")]
		public bool InStock { get; set; }

		public int? CategoryId { get; set; }

		public Category Category { get; set; }

		public List<PieReview> PieReviews  { get; set; }
	}
}