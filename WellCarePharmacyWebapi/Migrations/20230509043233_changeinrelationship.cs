using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WellCarePharmacyWebapi.Migrations
{
    public partial class changeinrelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductsId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ProductsId",
                table: "OrdersProducts",
                column: "ProductsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersProducts");

            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductsId",
                table: "Orders",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductsId",
                table: "Orders",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
