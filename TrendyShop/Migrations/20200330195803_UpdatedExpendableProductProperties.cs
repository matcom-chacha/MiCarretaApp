using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class UpdatedExpendableProductProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PackExpedables");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PackExpedables",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "LastInsertedQuantity",
                table: "PackExpedables",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastInsertionDate",
                table: "PackExpedables",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<float>(
                name: "TotalQuantity",
                table: "PackExpedables",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpendableProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasurement",
                table: "ExpendableProducts",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost_Name",
                table: "PackExpedables",
                columns: new[] { "Cost", "Name" },
                principalTable: "ExpendableProducts",
                principalColumns: new[] { "Cost", "Name" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost_Name",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "LastInsertedQuantity",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "LastInsertionDate",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasurement",
                table: "ExpendableProducts");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PackExpedables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExpendableProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables",
                column: "Cost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts",
                column: "Cost");

            migrationBuilder.AddForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost",
                table: "PackExpedables",
                column: "Cost",
                principalTable: "ExpendableProducts",
                principalColumn: "Cost",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
