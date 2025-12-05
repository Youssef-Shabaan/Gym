using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DesNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberPlans_plans_PlanId",
                table: "memberPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_sessions_SessionId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_plans_trainers_TrainerId",
                table: "plans");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_plans_PlanId",
                table: "sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "plans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_memberPlans_plans_PlanId",
                table: "memberPlans",
                column: "PlanId",
                principalTable: "plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_payments_sessions_SessionId",
                table: "payments",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_plans_trainers_TrainerId",
                table: "plans",
                column: "TrainerId",
                principalTable: "trainers",
                principalColumn: "TrainerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_plans_PlanId",
                table: "sessions",
                column: "PlanId",
                principalTable: "plans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_memberPlans_plans_PlanId",
                table: "memberPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_payments_sessions_SessionId",
                table: "payments");

            migrationBuilder.DropForeignKey(
                name: "FK_plans_trainers_TrainerId",
                table: "plans");

            migrationBuilder.DropForeignKey(
                name: "FK_sessions_plans_PlanId",
                table: "sessions");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "plans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_memberPlans_plans_PlanId",
                table: "memberPlans",
                column: "PlanId",
                principalTable: "plans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_payments_sessions_SessionId",
                table: "payments",
                column: "SessionId",
                principalTable: "sessions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_plans_trainers_TrainerId",
                table: "plans",
                column: "TrainerId",
                principalTable: "trainers",
                principalColumn: "TrainerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sessions_plans_PlanId",
                table: "sessions",
                column: "PlanId",
                principalTable: "plans",
                principalColumn: "Id");
        }
    }
}
