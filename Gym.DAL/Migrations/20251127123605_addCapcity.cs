using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addCapcity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "trainers");

            migrationBuilder.AddColumn<int>(
                name: "Capactiy",
                table: "sessions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Capcity",
                table: "plans",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capactiy",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "Capcity",
                table: "plans");

            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
