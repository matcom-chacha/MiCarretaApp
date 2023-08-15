using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedPOTypeToReplenishEProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "POTypeId",
                table: "ReplenishInfoEProds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoEProds_POTypeId",
                table: "ReplenishInfoEProds",
                column: "POTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplenishInfoEProds_ProductOperationTypes_POTypeId",
                table: "ReplenishInfoEProds",
                column: "POTypeId",
                principalTable: "ProductOperationTypes",
                principalColumn: "POTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplenishInfoEProds_ProductOperationTypes_POTypeId",
                table: "ReplenishInfoEProds");

            migrationBuilder.DropIndex(
                name: "IX_ReplenishInfoEProds_POTypeId",
                table: "ReplenishInfoEProds");

            migrationBuilder.DropColumn(
                name: "POTypeId",
                table: "ReplenishInfoEProds");
        }
    }
}
