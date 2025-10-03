using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamePaymentsAndMemberSessionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_members_memberId",
                table: "MemberSession");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberSession_sessions_sessionId",
                table: "MemberSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession");

            migrationBuilder.RenameTable(
                name: "MemberSession",
                newName: "memberSessions");

            migrationBuilder.RenameIndex(
                name: "IX_MemberSession_sessionId",
                table: "memberSessions",
                newName: "IX_memberSessions_sessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions",
                columns: new[] { "memberId", "sessionId" });

            migrationBuilder.CreateTable(
                name: "attendances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    memberId = table.Column<int>(type: "int", nullable: false),
                    isPresent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_attendances", x => x.id);
                    table.ForeignKey(
                        name: "FK_attendances_members_memberId",
                        column: x => x.memberId,
                        principalTable: "members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_attendances_memberId",
                table: "attendances",
                column: "memberId");

            migrationBuilder.AddForeignKey(
                name: "FK_memberSessions_members_memberId",
                table: "memberSessions",
                column: "memberId",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_memberSessions_sessions_sessionId",
                table: "memberSessions",
                column: "sessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_members_memberId",
                table: "memberSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_memberSessions_sessions_sessionId",
                table: "memberSessions");

            migrationBuilder.DropTable(
                name: "attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_memberSessions",
                table: "memberSessions");

            migrationBuilder.RenameTable(
                name: "memberSessions",
                newName: "MemberSession");

            migrationBuilder.RenameIndex(
                name: "IX_memberSessions_sessionId",
                table: "MemberSession",
                newName: "IX_MemberSession_sessionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MemberSession",
                table: "MemberSession",
                columns: new[] { "memberId", "sessionId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_members_memberId",
                table: "MemberSession",
                column: "memberId",
                principalTable: "members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberSession_sessions_sessionId",
                table: "MemberSession",
                column: "sessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
