using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class comments1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Semesters_Id",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "SemesterId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SemesterId",
                table: "Comments",
                column: "SemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Semesters_SemesterId",
                table: "Comments",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Semesters_SemesterId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_SemesterId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Comments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Semesters_Id",
                table: "Comments",
                column: "Id",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
