using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class CostIsFloatGProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackGastronomics_GastronomicProducts_Cost_Name",
                table: "PackGastronomics");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePerLodgings_GastronomicProducts_Cost_Name",
                table: "PurchasePerLodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomProducts_GastronomicProducts_Cost_Name",
                table: "RoomProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_GastronomicProducts_Cost_Name",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Storage_Cost_Name",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts");

            migrationBuilder.DropIndex(
                name: "IX_RoomProducts_Cost_Name",
                table: "RoomProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings");

            migrationBuilder.DropIndex(
                name: "IX_PurchasePerLodgings_Cost_Name",
                table: "PurchasePerLodgings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GastronomicProducts",
                table: "GastronomicProducts");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "Storage",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "RoomProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "PurchasePerLodgings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "PackGastronomics",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "GastronomicProducts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts",
                columns: new[] { "Name", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings",
                columns: new[] { "Name", "RoomId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GastronomicProducts",
                table: "GastronomicProducts",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GastronomicProducts",
                table: "GastronomicProducts");

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "Storage",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "RoomProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "PurchasePerLodgings",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "PackGastronomics",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "Cost",
                table: "GastronomicProducts",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                columns: new[] { "Name", "Cost" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts",
                columns: new[] { "Name", "Cost", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings",
                columns: new[] { "Name", "Cost", "RoomId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GastronomicProducts",
                table: "GastronomicProducts",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_Cost_Name",
                table: "Storage",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomProducts_Cost_Name",
                table: "RoomProducts",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePerLodgings_Cost_Name",
                table: "PurchasePerLodgings",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.AddForeignKey(
                name: "FK_PackGastronomics_GastronomicProducts_Cost_Name",
                table: "PackGastronomics",
                columns: new[] { "Cost", "Name" },
                principalTable: "GastronomicProducts",
                principalColumns: new[] { "Cost", "Name" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePerLodgings_GastronomicProducts_Cost_Name",
                table: "PurchasePerLodgings",
                columns: new[] { "Cost", "Name" },
                principalTable: "GastronomicProducts",
                principalColumns: new[] { "Cost", "Name" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomProducts_GastronomicProducts_Cost_Name",
                table: "RoomProducts",
                columns: new[] { "Cost", "Name" },
                principalTable: "GastronomicProducts",
                principalColumns: new[] { "Cost", "Name" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_GastronomicProducts_Cost_Name",
                table: "Storage",
                columns: new[] { "Cost", "Name" },
                principalTable: "GastronomicProducts",
                principalColumns: new[] { "Cost", "Name" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
