using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Asp.NetCoreMVC.Migrations
{
	public partial class Initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "AspNetRoles",
					columns: table => new {
						Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRoles", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUsers",
					columns: table => new {
						Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
						UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
						PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
						SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
						TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
						LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
						LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
						AccessFailedCount = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUsers", x => x.Id);
					});

			migrationBuilder.CreateTable(
					name: "Category",
					columns: table => new {
						CategoryId = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
						Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Category", x => x.CategoryId);
					});

			migrationBuilder.CreateTable(
					name: "AspNetRoleClaims",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
											column: x => x.RoleId,
											principalTable: "AspNetRoles",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserClaims",
					columns: table => new {
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
						table.ForeignKey(
											name: "FK_AspNetUserClaims_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserLogins",
					columns: table => new {
						LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
						ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
						ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
						table.ForeignKey(
											name: "FK_AspNetUserLogins_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserRoles",
					columns: table => new {
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
						table.ForeignKey(
											name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
											column: x => x.RoleId,
											principalTable: "AspNetRoles",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AspNetUserRoles_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "AspNetUserTokens",
					columns: table => new {
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
						LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
						Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
						Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
						table.ForeignKey(
											name: "FK_AspNetUserTokens_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "Order",
					columns: table => new {
						OrderId = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
						City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
						Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
						ShipperType = table.Column<string>(type: "nvarchar(50)", nullable: false),
						OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
						UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
						OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_Order", x => x.OrderId);
						table.ForeignKey(
											name: "FK_Order_AspNetUsers_UserId",
											column: x => x.UserId,
											principalTable: "AspNetUsers",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.CreateTable(
					name: "Pie",
					columns: table => new {
						PieId = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
						ShortDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
						LongDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
						AllergyInformation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
						Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
						ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
						ImageThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
						IsPieOfTheWeek = table.Column<bool>(type: "bit", nullable: false),
						InStock = table.Column<bool>(type: "bit", nullable: false),
						CategoryId = table.Column<int>(type: "int", nullable: true)
					},
					constraints: table => {
						table.PrimaryKey("PK_Pie", x => x.PieId);
						table.ForeignKey(
											name: "FK_Pie_Category_CategoryId",
											column: x => x.CategoryId,
											principalTable: "Category",
											principalColumn: "CategoryId",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.CreateTable(
					name: "OrderDetail",
					columns: table => new {
						OrderDetailId = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						OrderId = table.Column<int>(type: "int", nullable: false),
						PieId = table.Column<int>(type: "int", nullable: false),
						Amount = table.Column<int>(type: "int", nullable: false),
						Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_OrderDetail", x => x.OrderDetailId);
						table.ForeignKey(
											name: "FK_OrderDetail_Order_OrderId",
											column: x => x.OrderId,
											principalTable: "Order",
											principalColumn: "OrderId",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_OrderDetail_Pie_PieId",
											column: x => x.PieId,
											principalTable: "Pie",
											principalColumn: "PieId",
											onDelete: ReferentialAction.Cascade);
					});

			migrationBuilder.CreateTable(
					name: "ShoppingCartItem",
					columns: table => new {
						ShoppingCartItemId = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						PieId = table.Column<int>(type: "int", nullable: true),
						Amount = table.Column<int>(type: "int", nullable: false),
						ShoppingCartId = table.Column<string>(type: "nvarchar(100)", nullable: false)
					},
					constraints: table => {
						table.PrimaryKey("PK_ShoppingCartItem", x => x.ShoppingCartItemId);
						table.ForeignKey(
											name: "FK_ShoppingCartItem_Pie_PieId",
											column: x => x.PieId,
											principalTable: "Pie",
											principalColumn: "PieId",
											onDelete: ReferentialAction.Restrict);
					});

			migrationBuilder.InsertData(
					table: "Category",
					columns: new[] { "CategoryId", "CategoryName", "Description" },
					values: new object[] { 1, "Fruit pies", "All-fruity pies" });

			migrationBuilder.InsertData(
					table: "Category",
					columns: new[] { "CategoryId", "CategoryName", "Description" },
					values: new object[] { 2, "Cheese cakes", "Cheesy all the way" });

			migrationBuilder.InsertData(
					table: "Category",
					columns: new[] { "CategoryId", "CategoryName", "Description" },
					values: new object[] { 3, "Seasonal pies", "Get in the mood for a seasonal pie" });

			migrationBuilder.InsertData(
					table: "Pie",
					columns: new[] { "PieId", "AllergyInformation", "CategoryId", "ImageThumbnailUrl", "ImageUrl", "InStock", "IsPieOfTheWeek", "LongDescription", "Name", "Price", "ShortDescription" },
					values: new object[,]
					{
										{ 1, null, 1, "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg", "https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", true, false, "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée brownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie sweet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie lollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée gummies.", "Strawberry Pie", 15.95m, "Lorem Ipsum" },
										{ 3, null, 1, "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg", "https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", true, true, "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée rownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie weet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie ollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée ummies.", "Rhubarb Pie", 15.95m, "Lorem Ipsum" },
										{ 2, null, 2, "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg", "https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg", false, false, "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée rownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie weet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie ollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée ummies.", "Cheese cake", 18.95m, "Lorem Ipsum" },
										{ 4, null, 3, "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg", "https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg", true, true, "Icing carrot cake jelly-o cheesecake. Sweet roll marzipan marshmallow toffee brownie brownie candy tootsie roll. Chocolate cake gingerbread tootsie roll oat cake pie chocolate bar cookie dragée rownie. Lollipop cotton candy cake bear claw oat cake. Dragée candy canes dessert tart. Marzipan dragée gummies lollipop jujubes chocolate bar candy canes. Icing gingerbread chupa chups cotton candy cookie weet icing bonbon gummies. Gummies lollipop brownie biscuit danish chocolate cake. Danish powder cookie macaroon chocolate donut tart. Carrot cake dragée croissant lemon drops liquorice lemon drops cookie ollipop toffee. Carrot cake carrot cake liquorice sugar plum topping bonbon pie muffin jujubes. Jelly pastry wafer tart caramels bear claw. Tiramisu tart pie cake danish lemon drops. Brownie cupcake dragée ummies.", "Pumpkin Pie", 12.95m, "Lorem Ipsum" }
					});

			migrationBuilder.CreateIndex(
					name: "IX_AspNetRoleClaims_RoleId",
					table: "AspNetRoleClaims",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "RoleNameIndex",
					table: "AspNetRoles",
					column: "NormalizedName",
					unique: true,
					filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserClaims_UserId",
					table: "AspNetUserClaims",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserLogins_UserId",
					table: "AspNetUserLogins",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_AspNetUserRoles_RoleId",
					table: "AspNetUserRoles",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "EmailIndex",
					table: "AspNetUsers",
					column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
					name: "UserNameIndex",
					table: "AspNetUsers",
					column: "NormalizedUserName",
					unique: true,
					filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_Order_UserId",
					table: "Order",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_OrderDetail_OrderId",
					table: "OrderDetail",
					column: "OrderId");

			migrationBuilder.CreateIndex(
					name: "IX_OrderDetail_PieId",
					table: "OrderDetail",
					column: "PieId");

			migrationBuilder.CreateIndex(
					name: "IX_Pie_CategoryId",
					table: "Pie",
					column: "CategoryId");

			migrationBuilder.CreateIndex(
					name: "IX_ShoppingCartItem_PieId",
					table: "ShoppingCartItem",
					column: "PieId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "AspNetRoleClaims");

			migrationBuilder.DropTable(
					name: "AspNetUserClaims");

			migrationBuilder.DropTable(
					name: "AspNetUserLogins");

			migrationBuilder.DropTable(
					name: "AspNetUserRoles");

			migrationBuilder.DropTable(
					name: "AspNetUserTokens");

			migrationBuilder.DropTable(
					name: "OrderDetail");

			migrationBuilder.DropTable(
					name: "ShoppingCartItem");

			migrationBuilder.DropTable(
					name: "AspNetRoles");

			migrationBuilder.DropTable(
					name: "Order");

			migrationBuilder.DropTable(
					name: "Pie");

			migrationBuilder.DropTable(
					name: "AspNetUsers");

			migrationBuilder.DropTable(
					name: "Category");
		}
	}
}