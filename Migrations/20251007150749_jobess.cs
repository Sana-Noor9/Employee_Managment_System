using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class jobess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications");

            migrationBuilder.AddColumn<DateTime>(
                name: "EvaluationDate",
                table: "PerformanceEvaluations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "EvaluationDate",
                table: "PerformanceEvaluations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
