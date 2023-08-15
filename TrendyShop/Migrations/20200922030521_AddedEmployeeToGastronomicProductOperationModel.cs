using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedEmployeeToGastronomicProductOperationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "GastronomicProductOperations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GastronomicProductOperations_EmployeeId",
                table: "GastronomicProductOperations",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GastronomicProductOperations_Employees_EmployeeId",
                table: "GastronomicProductOperations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GastronomicProductOperations_Employees_EmployeeId",
                table: "GastronomicProductOperations");

            migrationBuilder.DropIndex(
                name: "IX_GastronomicProductOperations_EmployeeId",
                table: "GastronomicProductOperations");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "GastronomicProductOperations");
        }
    }
}
