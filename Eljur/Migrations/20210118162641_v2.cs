using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Eljur.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecializationId",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationLevels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationLevels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    EducationLevelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationDepartments_EducationLevels_EducationLevelId",
                        column: x => x.EducationLevelId,
                        principalTable: "EducationLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    EducationDepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_EducationDepartments_EducationDepartmentId",
                        column: x => x.EducationDepartmentId,
                        principalTable: "EducationDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Group_SpecializationId",
                table: "Group",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationDepartments_EducationLevelId",
                table: "EducationDepartments",
                column: "EducationLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_EducationDepartmentId",
                table: "Specializations",
                column: "EducationDepartmentId");

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

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "EducationDepartments");

            migrationBuilder.DropTable(
                name: "EducationLevels");

            migrationBuilder.DropIndex(
                name: "IX_Group_SpecializationId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Group");
        }
    }
}
