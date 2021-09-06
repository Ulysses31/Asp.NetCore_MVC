using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("OrderDetail")]
	public class OrderDetail
	{
		[Key]
		[Column("OrderDetailId", TypeName = "int")]
		public int OrderDetailId { get; set; }

		[Column("OrderId", TypeName = "int")]
		[Required]
		public int OrderId { get; set; }

		[Column("PieId", TypeName = "int")]
		[Required]
		public int PieId { get; set; }

		[Column("Amount", TypeName = "int")]
		[Required]
		public int Amount { get; set; }

		[Column("Price", TypeName = "decimal(18,2)")]
		[Required]
		public decimal Price { get; set; }

		public Pie Pie { get; set; }
		public Order Order { get; set; }
	}
}