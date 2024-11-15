using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supply.Api.Migrations
{
    /// <inheritdoc />
    public partial class AllUpdated1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrdersList_Product_Product_Id1",
                table: "ProductOrdersList");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrdersList_Product_Id1",
                table: "ProductOrdersList");

            migrationBuilder.DropColumn(
                name: "Product_Id1",
                table: "ProductOrdersList");

            migrationBuilder.AlterColumn<string>(
                name: "Product_Id",
                table: "ProductOrdersList",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrdersList_Product_Id",
                table: "ProductOrdersList",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrdersList_Product_Product_Id",
                table: "ProductOrdersList",
                column: "Product_Id",
                principalTable: "Product",
                principalColumn: "Product_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrdersList_Product_Product_Id",
                table: "ProductOrdersList");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrdersList_Product_Id",
                table: "ProductOrdersList");

            migrationBuilder.AlterColumn<string>(
                name: "Product_Id",
                table: "ProductOrdersList",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Product_Id1",
                table: "ProductOrdersList",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrdersList_Product_Id1",
                table: "ProductOrdersList",
                column: "Product_Id1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrdersList_Product_Product_Id1",
                table: "ProductOrdersList",
                column: "Product_Id1",
                principalTable: "Product",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
