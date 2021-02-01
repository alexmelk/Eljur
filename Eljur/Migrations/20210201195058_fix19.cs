using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class fix19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "StudentVisit",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "GroupVisit",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentVisit_ThemeId",
                table: "StudentVisit",
                column: "ThemeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Theme_ThemeId",
                table: "StudentVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeGroup_Theme_Id",
                table: "ThemeGroup",
                column: "Id",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Theme_ThemeId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_ThemeGroup_Theme_Id",
                table: "ThemeGroup");

            migrationBuilder.DropIndex(
                name: "IX_StudentVisit_ThemeId",
                table: "StudentVisit");

            migrationBuilder.DropIndex(
                name: "IX_GroupVisit_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "StudentVisit");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "GroupVisit");

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
    }
}
