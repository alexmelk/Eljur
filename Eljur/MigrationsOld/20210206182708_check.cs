using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Semesters",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Checks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: true),
                    SemesterId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checks_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Semesters_SemesterId",
                table: "Semesters",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Checks_SemesterId",
                table: "Checks",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Semesters_SemesterId",
                table: "Semesters",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Semesters_SemesterId",
                table: "Semesters");

            migrationBuilder.DropTable(
                name: "Checks");

            migrationBuilder.DropIndex(
                name: "IX_Semesters_SemesterId",
                table: "Semesters");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Semesters");
        }
    }
}
