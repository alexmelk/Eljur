using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropIndex(
                name: "IX_GroupVisit_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "GroupVisit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "GroupVisit",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupVisit_ThemeId",
                table: "GroupVisit",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
