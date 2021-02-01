using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class fix22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeGroup_Theme_Id",
                table: "ThemeGroup");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ThemeGroup",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Theme_ThemeGroupId",
                table: "Theme",
                column: "ThemeGroupId");

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

            migrationBuilder.DropIndex(
                name: "IX_Theme_ThemeGroupId",
                table: "Theme");

            migrationBuilder.DropColumn(
                name: "ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ThemeGroup",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeGroup_Theme_Id",
                table: "ThemeGroup",
                column: "Id",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
