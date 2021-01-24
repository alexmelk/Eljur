using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class addsemesters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
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
                name: "SubjectId",
                table: "StudentVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "GroupVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "GroupVisit",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Semesters_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "IX_Theme_SemesterId",
                table: "Theme",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupVisit_SemesterId",
                table: "GroupVisit",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_GroupId",
                table: "Semesters",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SemesterSubject_SubjectsId",
                table: "SemesterSubject",
                column: "SubjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit",
                column: "SubjectId",
                principalTable: "Subject",
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
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme",
                column: "ThemeGroupId",
                principalTable: "ThemeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Semesters_SemesterId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Semesters_SemesterId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme");

            migrationBuilder.DropTable(
                name: "SemesterSubject");

            migrationBuilder.DropTable(
                name: "Semesters");

            migrationBuilder.DropIndex(
                name: "IX_Theme_SemesterId",
                table: "Theme");

            migrationBuilder.DropIndex(
                name: "IX_GroupVisit_SemesterId",
                table: "GroupVisit");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "GroupVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "StudentVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme",
                column: "ThemeGroupId",
                principalTable: "ThemeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
