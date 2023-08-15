using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class ModifiedMoneyOpeationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MOTypeId",
                table: "MoneyOperations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOperations_MOTypeId",
                table: "MoneyOperations",
                column: "MOTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyOperations_MOTypes_MOTypeId",
                table: "MoneyOperations",
                column: "MOTypeId",
                principalTable: "MOTypes",
                principalColumn: "MOTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoneyOperations_MOTypes_MOTypeId",
                table: "MoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_MoneyOperations_MOTypeId",
                table: "MoneyOperations");

            migrationBuilder.DropColumn(
                name: "MOTypeId",
                table: "MoneyOperations");
        }
    }
}
