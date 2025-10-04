using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DropTableSessionName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sessions_sessionNames_SessionNameId",
                table: "sessions");

            migrationBuilder.DropTable(
                name: "sessionNames");

            migrationBuilder.DropIndex(
                name: "IX_sessions_SessionNameId",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "SessionNameId",
                table: "sessions");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "sessions");

            migrationBuilder.AddColumn<int>(
                name: "SessionNameId",
                table: "sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "sessionNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sessionNames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sessions_SessionNameId",
                table: "sessions",
                column: "SessionNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_sessionNames_SessionNameId",
                table: "sessions",
                column: "SessionNameId",
                principalTable: "sessionNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
