using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class globalfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Subject_SubjectId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Student_StudentId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Subject_SubjectId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Theme_ThemeId",
                table: "Visit");

            migrationBuilder.DropIndex(
                name: "IX_Visit_StudentId",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Visit");

            migrationBuilder.DropColumn(
                name: "TypeVisit",
                table: "Visit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Visit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Visit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Theme",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Student",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "StudentVisit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StudentId = table.Column<int>(nullable: false),
                    GroupVisitId = table.Column<int>(nullable: false),
                    TypeVisit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentVisit_Visit_GroupVisitId",
                        column: x => x.GroupVisitId,
                        principalTable: "Visit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentVisit_Student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Student",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisit_GroupVisitId",
                table: "StudentVisit",
                column: "GroupVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisit_StudentId",
                table: "StudentVisit",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Subject_SubjectId",
                table: "Theme",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Subject_SubjectId",
                table: "Visit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Theme_ThemeId",
                table: "Visit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student");

            migrationBuilder.DropForeignKey(
                name: "FK_Theme_Subject_SubjectId",
                table: "Theme");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Subject_SubjectId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Theme_ThemeId",
                table: "Visit");

            migrationBuilder.DropTable(
                name: "StudentVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Visit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Visit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Visit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeVisit",
                table: "Visit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "Theme",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Student",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Visit_StudentId",
                table: "Visit",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Group_GroupId",
                table: "Student",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_Subject_SubjectId",
                table: "Theme",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Student_StudentId",
                table: "Visit",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Subject_SubjectId",
                table: "Visit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Theme_ThemeId",
                table: "Visit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
