using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addsem1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDepartmentEducationLevel");

            migrationBuilder.AddColumn<int>(
                name: "EducationLevelsId",
                table: "EducationDepartments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationDepartments_EducationLevelsId",
                table: "EducationDepartments",
                column: "EducationLevelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EducationDepartments_EducationLevels_EducationLevelsId",
                table: "EducationDepartments",
                column: "EducationLevelsId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationDepartments_EducationLevels_EducationLevelsId",
                table: "EducationDepartments");

            migrationBuilder.DropIndex(
                name: "IX_EducationDepartments_EducationLevelsId",
                table: "EducationDepartments");

            migrationBuilder.DropColumn(
                name: "EducationLevelsId",
                table: "EducationDepartments");

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
                name: "IX_EducationDepartmentEducationLevel_EducationLevelsId",
                table: "EducationDepartmentEducationLevel",
                column: "EducationLevelsId");
        }
    }
}
