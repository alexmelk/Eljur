using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Group",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Group",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
