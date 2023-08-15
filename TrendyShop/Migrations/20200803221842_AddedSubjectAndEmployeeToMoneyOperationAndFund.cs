using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedSubjectAndEmployeeToMoneyOperationAndFund : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "MoneyOperations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "MoneyOperations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "FundMoneyOperations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "FundMoneyOperations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyOperations_EmployeeId",
                table: "MoneyOperations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FundMoneyOperations_EmployeeId",
                table: "FundMoneyOperations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FundMoneyOperations_Employees_EmployeeId",
                table: "FundMoneyOperations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MoneyOperations_Employees_EmployeeId",
                table: "MoneyOperations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FundMoneyOperations_Employees_EmployeeId",
                table: "FundMoneyOperations");

            migrationBuilder.DropForeignKey(
                name: "FK_MoneyOperations_Employees_EmployeeId",
                table: "MoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_MoneyOperations_EmployeeId",
                table: "MoneyOperations");

            migrationBuilder.DropIndex(
                name: "IX_FundMoneyOperations_EmployeeId",
                table: "FundMoneyOperations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "MoneyOperations");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "MoneyOperations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "FundMoneyOperations");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "FundMoneyOperations");
        }
    }
}
