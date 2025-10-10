using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class noo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_LeavesTypes_LeaveTypeId",
                table: "LeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_LeavesTypes_AspNetUsers_UserId",
                table: "LeavesTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeavesTypes",
                table: "LeavesTypes");

            migrationBuilder.RenameTable(
                name: "LeavesTypes",
                newName: "LeaveTypes");

            migrationBuilder.RenameIndex(
                name: "IX_LeavesTypes_UserId",
                table: "LeaveTypes",
                newName: "IX_LeaveTypes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_LeaveTypes_LeaveTypeId",
                table: "LeaveApplications",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "LeaveTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_UserId",
                table: "LeaveTypes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeaveApplications_LeaveTypes_LeaveTypeId",
                table: "LeaveApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_LeaveTypes_AspNetUsers_UserId",
                table: "LeaveTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeaveTypes",
                table: "LeaveTypes");

            migrationBuilder.RenameTable(
                name: "LeaveTypes",
                newName: "LeavesTypes");

            migrationBuilder.RenameIndex(
                name: "IX_LeaveTypes_UserId",
                table: "LeavesTypes",
                newName: "IX_LeavesTypes_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeavesTypes",
                table: "LeavesTypes",
                column: "LeaveTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeaveApplications_LeavesTypes_LeaveTypeId",
                table: "LeaveApplications",
                column: "LeaveTypeId",
                principalTable: "LeavesTypes",
                principalColumn: "LeaveTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LeavesTypes_AspNetUsers_UserId",
                table: "LeavesTypes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
