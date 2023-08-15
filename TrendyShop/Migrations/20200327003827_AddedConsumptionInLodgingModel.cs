using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedConsumptionInLodgingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "ExtraCharge",
                table: "Lodgings",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "Consumption",
                table: "Lodgings",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "RentCost",
                table: "Lodgings",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Consumption",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "RentCost",
                table: "Lodgings");

            migrationBuilder.AlterColumn<float>(
                name: "ExtraCharge",
                table: "Lodgings",
                type: "real",
                nullable: false,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}
