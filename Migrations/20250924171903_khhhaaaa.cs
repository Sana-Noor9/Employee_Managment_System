using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class khhhaaaa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_UserId1",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Salaries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Salaries",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_UserId",
                table: "Salaries",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId",
                table: "Salaries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId",
                table: "Salaries");

            migrationBuilder.DropIndex(
                name: "IX_Salaries_UserId",
                table: "Salaries");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Salaries",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Salaries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salaries_UserId1",
                table: "Salaries",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_AspNetUsers_UserId1",
                table: "Salaries",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
