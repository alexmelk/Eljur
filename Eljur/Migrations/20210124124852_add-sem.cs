using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addsem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDepartments_EducationLevels_EducationLevelId",
                table: "EducationDepartments");

            migrationBuilder.DropIndex(
                name: "IX_EducationDepartments_EducationLevelId",
                table: "EducationDepartments");

            migrationBuilder.DropColumn(
                name: "EducationLevelId",
                table: "EducationDepartments");

            migrationBuilder.AddColumn<int>(
                name: "EducationLevelId",
                table: "Specializations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationDepartmentEducationLevel",
                columns: table => new
                {
                    EducationDepartmentsId = table.Column<int>(type: "integer", nullable: false),
                    EducationLevelsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDepartmentEducationLevel", x => new { x.EducationDepartmentsId, x.EducationLevelsId });
                    table.ForeignKey(
                        name: "FK_EducationDepartmentEducationLevel_EducationDepartments_Educ~",
                        column: x => x.EducationDepartmentsId,
                        principalTable: "EducationDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationDepartmentEducationLevel_EducationLevels_Education~",
                        column: x => x.EducationLevelsId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_EducationLevelId",
                table: "Specializations",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDepartmentEducationLevel_EducationLevelsId",
                table: "EducationDepartmentEducationLevel",
                column: "EducationLevelsId");

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

            migrationBuilder.DropTable(
                name: "EducationDepartmentEducationLevel");

            migrationBuilder.DropIndex(
                name: "IX_Specializations_EducationLevelId",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "EducationLevelId",
                table: "Specializations");

            migrationBuilder.AddColumn<int>(
                name: "EducationLevelId",
                table: "EducationDepartments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationDepartments_EducationLevelId",
                table: "EducationDepartments",
                column: "EducationLevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDepartments_EducationLevels_EducationLevelId",
                table: "EducationDepartments",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
