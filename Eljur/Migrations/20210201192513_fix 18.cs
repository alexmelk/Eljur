﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Eljur.Migrations
{
    public partial class fix18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeVisit_Theme_ThemeId",
                table: "ThemeVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "ThemeVisit",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeVisit_Theme_ThemeId",
                table: "ThemeVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ThemeVisit_Theme_ThemeId",
                table: "ThemeVisit");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "ThemeVisit",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ThemeVisit_Theme_ThemeId",
                table: "ThemeVisit",
                column: "ThemeId",
                principalTable: "Theme",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
