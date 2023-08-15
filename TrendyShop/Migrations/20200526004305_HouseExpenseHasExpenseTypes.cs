using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class HouseExpenseHasExpenseTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenseTypeId",
                table: "HouseExpenses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HouseExpenses_ExpenseTypeId",
                table: "HouseExpenses",
                column: "ExpenseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses");

            migrationBuilder.DropIndex(
                name: "IX_HouseExpenses_ExpenseTypeId",
                table: "HouseExpenses");

            migrationBuilder.DropColumn(
                name: "ExpenseTypeId",
                table: "HouseExpenses");
        }
    }
}
