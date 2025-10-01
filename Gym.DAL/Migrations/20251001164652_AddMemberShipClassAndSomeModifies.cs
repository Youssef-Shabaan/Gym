using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMemberShipClassAndSomeModifies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "members",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleltedOn",
                table: "members",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "members",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "members",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleltedOn",
                table: "members");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "members");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "members");

            migrationBuilder.AlterColumn<DateTime>(
                name: "JoinDate",
                table: "members",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
