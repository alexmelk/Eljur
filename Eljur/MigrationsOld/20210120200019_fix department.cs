using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fixdepartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationLevelId",
                table: "Specializations",
                type: "integer",
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
    }
}
