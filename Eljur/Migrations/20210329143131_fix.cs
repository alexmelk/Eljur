using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Semesters_SemesterId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Group_GroupId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterStudents_Student_StudentId",
                table: "SemesterStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationDepartments_EducationDepartmentId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Student_StudentId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Group_GroupId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "ThemeId",
                table: "ThemeGroup",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThemeGroupId",
                table: "Theme",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Subject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "Subject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Subject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GrafikForSrId",
                table: "Subject",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "StudentVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevelId",
                table: "Specializations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EducationDepartmentId",
                table: "Specializations",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SemesterStudents",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Semesters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Group",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "Checks",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Semesters_SemesterId",
                table: "Checks",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Group_GroupId",
                table: "Semesters",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterStudents_Student_StudentId",
                table: "SemesterStudents",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationDepartments_EducationDepartmentId",
                table: "Specializations",
                column: "EducationDepartmentId",
                principalTable: "EducationDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Student_StudentId",
                table: "StudentVisit",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId",
                principalTable: "GrafikForSrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Group_GroupId",
                table: "Subject",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Checks_Semesters_SemesterId",
                table: "Checks");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Semesters_Group_GroupId",
                table: "Semesters");

            migrationBuilder.DropForeignKey(
                name: "FK_SemesterStudents_Student_StudentId",
                table: "SemesterStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationDepartments_EducationDepartmentId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Student_StudentId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Group_GroupId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "ThemeId",
                table: "ThemeGroup");

            migrationBuilder.DropColumn(
                name: "ThemeGroupId",
                table: "Theme");

            migrationBuilder.AlterColumn<int>(
                name: "TeacherId",
                table: "Subject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "Subject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Subject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GrafikForSrId",
                table: "Subject",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "StudentVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "StudentVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevelId",
                table: "Specializations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EducationDepartmentId",
                table: "Specializations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "SemesterStudents",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Semesters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectId",
                table: "GroupVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "GroupVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SpecializationId",
                table: "Group",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "SemesterId",
                table: "Checks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Checks_Semesters_SemesterId",
                table: "Checks",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Specializations_SpecializationId",
                table: "Group",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Group_GroupId",
                table: "GroupVisit",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupVisit_Subject_SubjectId",
                table: "GroupVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Semesters_Group_GroupId",
                table: "Semesters",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SemesterStudents_Student_StudentId",
                table: "SemesterStudents",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationDepartments_EducationDepartmentId",
                table: "Specializations",
                column: "EducationDepartmentId",
                principalTable: "EducationDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specializations_EducationLevels_EducationLevelId",
                table: "Specializations",
                column: "EducationLevelId",
                principalTable: "EducationLevels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Student_StudentId",
                table: "StudentVisit",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentVisit_Subject_SubjectId",
                table: "StudentVisit",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_GrafikForSrs_GrafikForSrId",
                table: "Subject",
                column: "GrafikForSrId",
                principalTable: "GrafikForSrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Group_GroupId",
                table: "Subject",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Semesters_SemesterId",
                table: "Subject",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Teachers_TeacherId",
                table: "Subject",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
