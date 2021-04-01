using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class ашч227 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId");
        }
    }
}
