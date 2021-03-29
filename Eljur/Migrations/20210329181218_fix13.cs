using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject");

            migrationBuilder.AlterColumn<int>(
                name: "GrafikForSrId",
                table: "Subject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

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

            migrationBuilder.AlterColumn<int>(
                name: "GrafikForSrId",
                table: "Subject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId",
                principalTable: "GrafikForSrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
