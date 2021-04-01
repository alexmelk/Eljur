using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class IsChoosen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChoosen",
                table: "Theme",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChoosen",
                table: "Theme");
        }
    }
}
