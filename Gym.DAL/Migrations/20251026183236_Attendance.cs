using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Attendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_members_memberId",
                table: "attendances");

            migrationBuilder.RenameColumn(
                name: "memberId",
                table: "attendances",
                newName: "MemberId");

            migrationBuilder.RenameColumn(
                name: "isPresent",
                table: "attendances",
                newName: "IsPresent");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "attendances",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "attendances",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_memberId",
                table: "attendances",
                newName: "IX_attendances_MemberId");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "attendances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_attendances_SessionId",
                table: "attendances",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_members_MemberId",
                table: "attendances",
                column: "MemberId",
                principalTable: "members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_sessions_SessionId",
                table: "attendances",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attendances_members_MemberId",
                table: "attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_attendances_sessions_SessionId",
                table: "attendances");

            migrationBuilder.DropIndex(
                name: "IX_attendances_SessionId",
                table: "attendances");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "attendances");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "attendances",
                newName: "memberId");

            migrationBuilder.RenameColumn(
                name: "IsPresent",
                table: "attendances",
                newName: "isPresent");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "attendances",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "attendances",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_attendances_MemberId",
                table: "attendances",
                newName: "IX_attendances_memberId");

            migrationBuilder.AddForeignKey(
                name: "FK_attendances_members_memberId",
                table: "attendances",
                column: "memberId",
                principalTable: "members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
