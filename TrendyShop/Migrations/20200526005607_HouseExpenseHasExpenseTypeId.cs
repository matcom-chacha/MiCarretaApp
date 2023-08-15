using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class HouseExpenseHasExpenseTypeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseTypeId",
                table: "HouseExpenses",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses");

            migrationBuilder.AlterColumn<int>(
                name: "ExpenseTypeId",
                table: "HouseExpenses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_HouseExpenses_ExpenseTypes_ExpenseTypeId",
                table: "HouseExpenses",
                column: "ExpenseTypeId",
                principalTable: "ExpenseTypes",
                principalColumn: "ExpenseTypeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
