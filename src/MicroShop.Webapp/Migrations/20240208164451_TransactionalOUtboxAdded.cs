using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroShop.Webapp.Migrations
{
    /// <inheritdoc />
    public partial class TransactionalOUtboxAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransactionalOutbox",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tabella = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Messaggio = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionalOutbox", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionalOutbox");
        }
    }
}
