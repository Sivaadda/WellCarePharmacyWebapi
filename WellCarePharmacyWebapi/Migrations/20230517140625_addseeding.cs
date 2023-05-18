using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WellCarePharmacyWebapi.Migrations
{
    public partial class addseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Descripition", "Discount", "ImageUrl", "Price", "ProductName", "Status" },
                values: new object[,]
                {
                    { 3, "soap made with charcoal", 45m, "iwyer", 0f, "Active Charcoal Soap", "Available" },
                    { 4, "soap made with charcoal", 45m, "iwyer", 0f, "Active Soap", "NotAvailable" },
                    { 5, "soap made with charcoal", 45m, "iwyer", 0f, "Active Charcoal Soap", "Available" },
                    { 6, "soap made with charcoal", 45m, "iwyer", 0f, "Active Soap", "NotAvailable" },
                    { 7, "soap made with charcoal", 45m, "iwyer", 0f, "Active Charcoal Soap", "Available" },
                    { 8, "soap made with charcoal", 45m, "iwyer", 0f, "Active Soap", "NotAvailable" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "RoleName" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber", "RegisteredOn", "RoleId" },
                values: new object[] { 1, "siva@gmail.com", "siva", "12345", 99, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "PhoneNumber", "RegisteredOn", "RoleId" },
                values: new object[] { 2, "siva@gmail.com", "siva", "12345", 99, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity", "TotalPrice" },
                values: new object[] { 1, 1, 3, 3, 345m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
