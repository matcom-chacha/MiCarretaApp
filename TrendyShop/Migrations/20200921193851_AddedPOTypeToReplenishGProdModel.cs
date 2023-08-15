using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedPOTypeToReplenishGProdModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "POTypeId",
                table: "ReplenishInfoGProds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoGProds_POTypeId",
                table: "ReplenishInfoGProds",
                column: "POTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplenishInfoGProds_ProductOperationTypes_POTypeId",
                table: "ReplenishInfoGProds",
                column: "POTypeId",
                principalTable: "ProductOperationTypes",
                principalColumn: "POTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplenishInfoGProds_ProductOperationTypes_POTypeId",
                table: "ReplenishInfoGProds");

            migrationBuilder.DropIndex(
                name: "IX_ReplenishInfoGProds_POTypeId",
                table: "ReplenishInfoGProds");

            migrationBuilder.DropColumn(
                name: "POTypeId",
                table: "ReplenishInfoGProds");
        }
    }
}
