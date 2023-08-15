using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ExpProductHasAvailableAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "AmountInRooms",
                table: "Storage",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "AmountAvailable",
                table: "Storage",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<float>(
                name: "AmountAvailable",
                table: "ExpendableProducts",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountAvailable",
                table: "ExpendableProducts");

            migrationBuilder.AlterColumn<int>(
                name: "AmountInRooms",
                table: "Storage",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<int>(
                name: "AmountAvailable",
                table: "Storage",
                type: "int",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}
