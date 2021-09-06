using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("ShoppingCartItem")]
	public class ShoppingCartItem
	{
		[Key]
		[Column("ShoppingCartItemId")]
		public int ShoppingCartItemId { get; set; }

		public Pie Pie { get; set; }

		[Column("Amount", TypeName = "int")]
		[Required]
		public int Amount { get; set; }

		[Column("ShoppingCartId", TypeName = "nvarchar(100)")]
		[Required]
		public string ShoppingCartId { get; set; }
	}
}