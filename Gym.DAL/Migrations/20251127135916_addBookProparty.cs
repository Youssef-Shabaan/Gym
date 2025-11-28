using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addBookProparty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Booked",
                table: "sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Booked",
                table: "plans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Booked",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "Booked",
                table: "plans");
        }
    }
}
