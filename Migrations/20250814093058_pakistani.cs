using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class pakistani : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_UserId1",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_AspNetUsers_UserId1",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Salaries",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "PerformanceEvaluations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Leaves",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_UserId1",
                table: "Attendances",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_AspNetUsers_UserId1",
                table: "Leaves",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AspNetUsers_UserId1",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_AspNetUsers_UserId1",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Salaries",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "PerformanceEvaluations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Leaves",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId1",
                table: "Attendances",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AspNetUsers_UserId1",
                table: "Attendances",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_AspNetUsers_UserId1",
                table: "Leaves",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
