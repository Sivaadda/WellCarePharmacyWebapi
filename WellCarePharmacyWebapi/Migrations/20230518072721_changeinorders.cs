using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WellCarePharmacyWebapi.Migrations
{
    public partial class changeinorders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_OrderId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderId",
                table: "Orders",
                newName: "IX_Orders_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UsersId",
                table: "Orders",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UsersId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UsersId",
                table: "Orders",
                newName: "IX_Orders_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_OrderId",
                table: "Orders",
                column: "OrderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
