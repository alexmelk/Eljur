using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class namefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Visit_GroupVisitId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Group_GroupId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Subject_SubjectId",
                table: "Visit");

            migrationBuilder.DropForeignKey(
                name: "FK_Visit_Theme_ThemeId",
                table: "Visit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Visit",
                table: "Visit");

            migrationBuilder.RenameTable(
                name: "Visit",
                newName: "GroupVisit");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_ThemeId",
                table: "GroupVisit",
                newName: "IX_GroupVisit_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_SubjectId",
                table: "GroupVisit",
                newName: "IX_GroupVisit_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Visit_GroupId",
                table: "GroupVisit",
                newName: "IX_GroupVisit_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupVisit",
                table: "GroupVisit",
                column: "Id");

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
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_GroupVisit_GroupVisitId",
                table: "StudentVisit",
                column: "GroupVisitId",
                principalTable: "GroupVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_GroupVisit_GroupVisitId",
                table: "StudentVisit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupVisit",
                table: "GroupVisit");

            migrationBuilder.RenameTable(
                name: "GroupVisit",
                newName: "Visit");

            migrationBuilder.RenameIndex(
                name: "IX_GroupVisit_ThemeId",
                table: "Visit",
                newName: "IX_Visit_ThemeId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupVisit_SubjectId",
                table: "Visit",
                newName: "IX_Visit_SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupVisit_GroupId",
                table: "Visit",
                newName: "IX_Visit_GroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Visit",
                table: "Visit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Visit_GroupVisitId",
                table: "StudentVisit",
                column: "GroupVisitId",
                principalTable: "Visit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Visit_Group_GroupId",
                table: "Visit",
                column: "GroupId",
                principalTable: "Group",
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
    }
}
