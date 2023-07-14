using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalAppMVC.Migrations
{
    /// <inheritdoc />
    public partial class namesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentOrders_AspNetUsers_AppUserId",
                table: "RentOrders");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "RentOrders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentOrders_AppUserId",
                table: "RentOrders",
                newName: "IX_RentOrders_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentOrders_AspNetUsers_UserId",
                table: "RentOrders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentOrders_AspNetUsers_UserId",
                table: "RentOrders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "RentOrders",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_RentOrders_UserId",
                table: "RentOrders",
                newName: "IX_RentOrders_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RentOrders_AspNetUsers_AppUserId",
                table: "RentOrders",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
