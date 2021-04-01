using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class chngesubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SemesterSubject");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Subject",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_SemesterId",
                table: "Subject",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_SemesterId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "SemesterSubject",
                columns: table => new
                {
                    SemestersId = table.Column<int>(type: "integer", nullable: false),
                    SubjectsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SemesterSubject", x => new { x.SemestersId, x.SubjectsId });
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Semesters_SemestersId",
                        column: x => x.SemestersId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SemesterSubject_Subject_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SubjectsId",
                table: "SemesterSubject",
                column: "SubjectsId");
        }
    }
}
