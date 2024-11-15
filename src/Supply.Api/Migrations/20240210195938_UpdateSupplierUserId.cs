using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supply.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSupplierUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Supplier_Supplier_Id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Supplier_Supplier_Id",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Product_Supplier_Id",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_Supplier_Id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Supplier_Id",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "Supplier_Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Supplier_Id",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Supplier",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Supplier",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Supplier",
                newName: "Address");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Supplier",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Product",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Id",
                table: "Order",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_User_Id",
                table: "Product",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_User_Id",
                table: "Order",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Supplier_User_Id",
                table: "Order",
                column: "User_Id",
                principalTable: "Supplier",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Supplier_User_Id",
                table: "Product",
                column: "User_Id",
                principalTable: "Supplier",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Supplier_User_Id",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Product_Supplier_User_Id",
                table: "Product");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier");

            migrationBuilder.DropIndex(
                name: "IX_Product_User_Id",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Order_User_Id",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Supplier",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Supplier",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Supplier",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "Supplier_Id",
                table: "Supplier",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Supplier_Id",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Supplier_Id",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Supplier",
                table: "Supplier",
                column: "Supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Supplier_Id",
                table: "Product",
                column: "Supplier_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Supplier_Id",
                table: "Order",
                column: "Supplier_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Supplier_Supplier_Id",
                table: "Order",
                column: "Supplier_Id",
                principalTable: "Supplier",
                principalColumn: "Supplier_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Supplier_Supplier_Id",
                table: "Product",
                column: "Supplier_Id",
                principalTable: "Supplier",
                principalColumn: "Supplier_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
