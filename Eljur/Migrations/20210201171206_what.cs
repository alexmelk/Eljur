using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class what : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterStudents_Semesters_SemesterId",
                table: "SemesterStudents");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "SemesterStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterStudents_Semesters_SemesterId",
                table: "SemesterStudents",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SemesterStudents_Semesters_SemesterId",
                table: "SemesterStudents");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "SemesterStudents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterStudents_Semesters_SemesterId",
                table: "SemesterStudents",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
