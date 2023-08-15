using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ReadjustingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Reservations_RoomId_Date",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "FinishedOrCancelled",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Maintenances",
                newName: "Date");

            migrationBuilder.AlterColumn<float>(
                name: "StandardPrice",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "Maintenances",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "TotalPrice",
                table: "Lodgings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Prepaid",
                table: "Lodgings",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<float>(
                name: "ExtraCharge",
                table: "Lodgings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FinalDate",
                table: "Lodgings",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Incidences",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalDate",
                table: "Lodgings");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Maintenances",
                newName: "date");

            migrationBuilder.AlterColumn<int>(
                name: "StandardPrice",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<bool>(
                name: "FinishedOrCancelled",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "Maintenances",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Lodgings",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<bool>(
                name: "Prepaid",
                table: "Lodgings",
                type: "bit",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "ExtraCharge",
                table: "Lodgings",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Incidences",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Reservations_RoomId_Date",
                table: "Lodgings",
                columns: new[] { "RoomId", "Date" },
                principalTable: "Reservations",
                principalColumns: new[] { "RoomId", "Date" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
