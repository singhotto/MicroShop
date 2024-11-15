using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Webapp.Migrations
{
    /// <inheritdoc />
    public partial class CartItemProductUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cart_Product_Id",
                table: "Cart",
                column: "Product_Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cart_Product_Id",
                table: "Cart");
        }
    }
}
