using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class ThemeVisit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropColumn(
                name: "HoursPerVisit",
                table: "GroupVisit");

            migrationBuilder.DropColumn(
                name: "TypeSubject",
                table: "GroupVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "GroupVisit",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateTable(
                name: "ThemeVisit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoursPerVisit = table.Column<double>(nullable: false),
                    TypeSubject = table.Column<int>(nullable: false),
                    ThemeId = table.Column<int>(nullable: true),
                    GroupVisitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemeVisit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThemeVisit_GroupVisit_GroupVisitId",
                        column: x => x.GroupVisitId,
                        principalTable: "GroupVisit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ThemeVisit_Theme_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Theme",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThemeVisit_GroupVisitId",
                table: "ThemeVisit",
                column: "GroupVisitId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemeVisit_ThemeId",
                table: "ThemeVisit",
                column: "ThemeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit");

            migrationBuilder.DropTable(
                name: "ThemeVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HoursPerVisit",
                table: "GroupVisit",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "TypeSubject",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Theme_ThemeId",
                table: "GroupVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
