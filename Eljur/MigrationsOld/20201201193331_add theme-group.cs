using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addthemegroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

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
