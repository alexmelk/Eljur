using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Semesters_SemesterId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_ThemeVisit_GroupVisit_GroupVisitId",
                table: "ThemeVisit");

            migrationBuilder.DropIndex(
                name: "IX_Theme_SemesterId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "GroupVisitId",
                table: "ThemeVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeVisit_GroupVisit_GroupVisitId",
                table: "ThemeVisit",
                column: "GroupVisitId",
                principalTable: "GroupVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_ThemeVisit_GroupVisit_GroupVisitId",
                table: "ThemeVisit");

            migrationBuilder.AlterColumn<int>(
                name: "GroupVisitId",
                table: "ThemeVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Theme",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "GroupVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Theme_SemesterId",
                table: "Theme",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Semesters_SemesterId",
                table: "Theme",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeVisit_GroupVisit_GroupVisitId",
                table: "ThemeVisit",
                column: "GroupVisitId",
                principalTable: "GroupVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
