using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class ediSpec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationLevelId",
                table: "Specializations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_EducationLevelId",
                table: "Specializations",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_EducationLevelId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "EducationLevelId",
                table: "Specializations");
        }
    }
}
