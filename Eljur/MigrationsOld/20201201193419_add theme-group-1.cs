using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addthemegroup1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme",
                column: "ThemeGroupId",
                principalTable: "ThemeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Theme_ThemeGroup_ThemeGroupId",
                table: "Theme",
                column: "ThemeGroupId",
                principalTable: "ThemeGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
