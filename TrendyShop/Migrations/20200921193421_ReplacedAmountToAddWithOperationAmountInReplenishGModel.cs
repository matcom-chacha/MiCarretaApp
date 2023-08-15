using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ReplacedAmountToAddWithOperationAmountInReplenishGModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAddAmount",
                table: "ReplenishInfoGProds");

            migrationBuilder.AddColumn<float>(
                name: "OperationAmount",
                table: "ReplenishInfoGProds",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationAmount",
                table: "ReplenishInfoGProds");

            migrationBuilder.AddColumn<float>(
                name: "ToAddAmount",
                table: "ReplenishInfoGProds",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
