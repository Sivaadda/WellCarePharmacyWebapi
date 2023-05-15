using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WellCarePharmacyWebapi.Migrations
{
    public partial class addingseedingofproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descripition", "Discount", "ImageUrl", "Price", "ProductName", "Status" },
                values: new object[] { 7, "soap made with charcoal", 45m, "iwyer", 0f, "Active Charcoal Soap", "Available" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descripition", "Discount", "ImageUrl", "Price", "ProductName", "Status" },
                values: new object[] { 8, "soap made with charcoal", 45m, "iwyer", 0f, "Active Soap", "NotAvailable" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descripition", "Discount", "ImageUrl", "Price", "ProductName", "Status" },
                values: new object[] { 1, "soap made with charcoal", 45m, "iwyer", 0f, "Active Charcoal Soap", "Available" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descripition", "Discount", "ImageUrl", "Price", "ProductName", "Status" },
                values: new object[] { 2, "soap made with charcoal", 45m, "iwyer", 0f, "Active Soap", "NotAvailable" });
        }
    }
}
