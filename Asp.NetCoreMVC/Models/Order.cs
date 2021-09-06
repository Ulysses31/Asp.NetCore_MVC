using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.NetCoreMVC.Models
{
	[Table("Order")]
	public class Order
	{
		[Key]
		[BindNever]
		[Column("OrderId", TypeName = "int")]
		public int OrderId { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }

		[Column("FirstName", TypeName = "nvarchar(50)")]
		[Display(Name = "First Name")]
		[Required]
		[StringLength(50)]
		public string FirstName { get; set; }

		[Column("LastName", TypeName = "nvarchar(50)")]
		[Display(Name = "Last Name")]
		[Required]
		[StringLength(50)]
		public string LastName { get; set; }

		[Column("AddressLine1", TypeName = "nvarchar(100)")]
		[Display(Name = "Address Line 1")]
		[Required]
		[StringLength(100)]
		public string AddressLine1 { get; set; }

		[Column("AddressLine2", TypeName = "nvarchar(100)")]
		[Display(Name = "Address Line 2")]
		[StringLength(100)]
		public string AddressLine2 { get; set; }

		[Column("ZipCode", TypeName = "nvarchar(10)")]
		[Display(Name = "ZipCode")]
		[Required]
		[StringLength(10, MinimumLength = 4)]
		public string ZipCode { get; set; }

		[Column("City", TypeName = "nvarchar(50)")]
		[Display(Name = "City")]
		[StringLength(50)]
		public string City { get; set; }

		[Column("State", TypeName = "nvarchar(50)")]
		[Display(Name = "State")]
		[StringLength(50)]
		public string State { get; set; }

		[Column("Country", TypeName = "nvarchar(50)")]
		[Display(Name = "Country")]
		[StringLength(50)]
		public string Country { get; set; }

		[Column("PhoneNumber", TypeName = "nvarchar(25)")]
		[Display(Name = "PhoneNumber")]
		[DataType(DataType.PhoneNumber)]
		[StringLength(25)]
		[Required]
		public string PhoneNumber { get; set; }

		[Column("Email", TypeName = "nvarchar(50)")]
		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		[StringLength(50)]
		[RegularExpression(@"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])", ErrorMessage = "The email address is not entered in a correct format")]
		[Required]
		public string Email { get; set; }

		[Column("Shipper", TypeName = "nvarchar(50)")]
		[Display(Name = "Shipper")]
		[Required]
		public Shipper Shipper { get; set; }

		public IEnumerable<SelectListItem> Shippers;

		[BindNever]
		[Column("OrderTotal", TypeName = "decimal(18,2)")]
		[ScaffoldColumn(false)]
		public decimal OrderTotal { get; set; }

		[ForeignKey("User")]
		[Column("UserId", TypeName = "nvarchar(450)")]
		public string UserId { get; set; }

		public IdentityUser User { get; set; }

		[BindNever]
		[ScaffoldColumn(false)]
		public DateTime OrderPlaced { get; set; }
	}
}