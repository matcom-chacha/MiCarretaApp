using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class HomePage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LodgingId",
                table: "Lodgings");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Lodgings",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Lodgings");

            migrationBuilder.AddColumn<int>(
                name: "LodgingId",
                table: "Lodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
