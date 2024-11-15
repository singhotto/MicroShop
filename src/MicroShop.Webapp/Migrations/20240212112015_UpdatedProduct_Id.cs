using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Webapp.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedProduct_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e8c13b1-ac9d-4d2a-aff7-77a67c113ea2");

            migrationBuilder.AlterColumn<string>(
                name: "Product_Id",
                table: "Cart",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Product_Id",
                table: "Cart",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8e8c13b1-ac9d-4d2a-aff7-77a67c113ea2", 0, "Via libera 20 roma", "8fe83da7-dea4-4680-b02a-5841ecaf6db2", "supplier@micro.it", true, "Supplier", "King", true, null, "SUPPLIER@MICRO.IT", "SUPPLIER@MICRO.IT", "AQAAAAIAAYagAAAAEOQFXXYc3gh7TUWjYcaab6Z50xBTxM70nTaXqHP3I7tw2L6yyKTV1Yny6cKM0ufoEg==", null, false, "USQN73AQLEVY6WYJIBRRQJ3VGDYDDEXL", false, "supplier@micro.it" });
        }
    }
}
