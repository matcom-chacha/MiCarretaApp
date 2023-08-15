using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ModifiedDateOfClosingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "DatesOfClosings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousClosingDate",
                table: "DatesOfClosings",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreviousClousingDate",
                table: "DatesOfClosings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DatesOfClosings_EmployeeId",
                table: "DatesOfClosings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DatesOfClosings_PreviousClosingDate",
                table: "DatesOfClosings",
                column: "PreviousClosingDate");

            migrationBuilder.AddForeignKey(
                name: "FK_DatesOfClosings_Employees_EmployeeId",
                table: "DatesOfClosings",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DatesOfClosings_DatesOfClosings_PreviousClosingDate",
                table: "DatesOfClosings",
                column: "PreviousClosingDate",
                principalTable: "DatesOfClosings",
                principalColumn: "Date",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatesOfClosings_Employees_EmployeeId",
                table: "DatesOfClosings");

            migrationBuilder.DropForeignKey(
                name: "FK_DatesOfClosings_DatesOfClosings_PreviousClosingDate",
                table: "DatesOfClosings");

            migrationBuilder.DropIndex(
                name: "IX_DatesOfClosings_EmployeeId",
                table: "DatesOfClosings");

            migrationBuilder.DropIndex(
                name: "IX_DatesOfClosings_PreviousClosingDate",
                table: "DatesOfClosings");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "DatesOfClosings");

            migrationBuilder.DropColumn(
                name: "PreviousClosingDate",
                table: "DatesOfClosings");

            migrationBuilder.DropColumn(
                name: "PreviousClousingDate",
                table: "DatesOfClosings");
        }
    }
}
