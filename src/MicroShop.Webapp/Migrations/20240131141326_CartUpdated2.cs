using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Webapp.Migrations
{
    /// <inheritdoc />
    public partial class CartUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_UserName",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Cart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserName",
                table: "Cart",
                newName: "IX_Cart_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_UserId",
                table: "Cart",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_UserId",
                table: "Cart");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                newName: "IX_Cart_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_UserName",
                table: "Cart",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
