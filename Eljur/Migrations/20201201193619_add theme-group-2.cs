using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class addthemegroup2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedHours",
                table: "ThemeGroup");

            migrationBuilder.AddColumn<int>(
                name: "AllowedHours",
                table: "Theme",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedHours",
                table: "Theme");

            migrationBuilder.AddColumn<int>(
                name: "AllowedHours",
                table: "ThemeGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
