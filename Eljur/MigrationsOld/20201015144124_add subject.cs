using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addsubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "StudentVisit",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisit_SubjectId",
                table: "StudentVisit",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropIndex(
                name: "IX_StudentVisit_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentVisit");
        }
    }
}
