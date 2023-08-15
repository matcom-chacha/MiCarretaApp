using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ReplacedAmountToAddForOperationAmountInReplenishEProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAddAmount",
                table: "ReplenishInfoEProds");

            migrationBuilder.AddColumn<float>(
                name: "OperationAmount",
                table: "ReplenishInfoEProds",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationAmount",
                table: "ReplenishInfoEProds");

            migrationBuilder.AddColumn<float>(
                name: "ToAddAmount",
                table: "ReplenishInfoEProds",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
