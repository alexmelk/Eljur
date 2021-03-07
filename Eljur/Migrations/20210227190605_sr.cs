using System.Collections.Generic;
using Eljur.Context.Tables;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class sr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GrafikForSrId",
                table: "Subject",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GrafikForSrs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    indepWorkEnums = table.Column<List<IndepWorkEnumRus>>(type: "integer[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrafikForSrs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId",
                principalTable: "GrafikForSrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "GrafikForSrs");

            migrationBuilder.DropIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "GrafikForSrId",
                table: "Subject");
        }
    }
}
