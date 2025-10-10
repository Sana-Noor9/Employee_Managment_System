using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class testes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceEvaluations_UserId1",
                table: "PerformanceEvaluations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PerformanceEvaluations");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PerformanceEvaluations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "PerformanceEvaluations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEvaluations_UserId",
                table: "PerformanceEvaluations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId",
                table: "PerformanceEvaluations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId",
                table: "PerformanceEvaluations");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceEvaluations_UserId",
                table: "PerformanceEvaluations");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "PerformanceEvaluations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "PerformanceEvaluations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "PerformanceEvaluations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceEvaluations_UserId1",
                table: "PerformanceEvaluations",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceEvaluations_AspNetUsers_UserId1",
                table: "PerformanceEvaluations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
