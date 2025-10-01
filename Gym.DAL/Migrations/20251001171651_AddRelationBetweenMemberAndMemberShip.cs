using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenMemberAndMemberShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberShipId",
                table: "members",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_members_MemberShipId",
                table: "members",
                column: "MemberShipId");

            migrationBuilder.AddForeignKey(
                name: "FK_members_memberShips_MemberShipId",
                table: "members",
                column: "MemberShipId",
                principalTable: "memberShips",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_members_memberShips_MemberShipId",
                table: "members");

            migrationBuilder.DropIndex(
                name: "IX_members_MemberShipId",
                table: "members");

            migrationBuilder.DropColumn(
                name: "MemberShipId",
                table: "members");
        }
    }
}
