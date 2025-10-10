using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class khannnnnnnn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_LeaveApplications_UserId1",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "LeaveApplications");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LeaveDays",
                table: "LeaveApplications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_UserId",
                table: "LeaveApplications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId",
                table: "LeaveApplications");

            migrationBuilder.DropIndex(
                name: "IX_LeaveApplications_UserId",
                table: "LeaveApplications");

            migrationBuilder.DropColumn(
                name: "LeaveDays",
                table: "LeaveApplications");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LeaveApplications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "LeaveApplications",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeaveApplications_UserId1",
                table: "LeaveApplications",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_AspNetUsers_UserId1",
                table: "LeaveApplications",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
