using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Webapp.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3fb56e65-45d5-425c-89a3-fa7129f29d87", 0, "Via Roma 20", "ec461138-2a37-440e-a06c-06143853efa7", "admin@micro.it", true, "Otto", "Killer", true, null, "ADMIN@MICRO.IT", "ADMIN@MICRO.IT", "AQAAAAIAAYagAAAAEDrxs5cA726kwKtlpro/RYIBXoSxsggXc+fiekOhhtB1o3HFq5P4DzZThlATpgM/RA==", null, false, "YJXDVNF4MHTFXJRJHIIY7GSN7AXWUTQW", false, "admin@micro.it" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3fb56e65-45d5-425c-89a3-fa7129f29d87");
        }
    }
}
