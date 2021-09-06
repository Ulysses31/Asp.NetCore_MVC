using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("Category")]
	public class Category
	{
		[Key]
		[Column("CategoryId")]
		public int CategoryId { get; set; }

		[Column("CategoryName", TypeName = "nvarchar(200)")]
		[MaxLength(200)]
		[Required()]
		public string CategoryName { get; set; }

		[Column("Description", TypeName = "nvarchar(255)")]
		[MaxLength(255)]
		[Required()]
		public string Description { get; set; }

		public List<Pie> Pies { get; set; }
	}
}