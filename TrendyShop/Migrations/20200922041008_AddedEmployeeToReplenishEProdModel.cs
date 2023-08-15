using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedEmployeeToReplenishEProdModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "ReplenishInfoEProds",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoEProds_EmployeeId",
                table: "ReplenishInfoEProds",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplenishInfoEProds_Employees_EmployeeId",
                table: "ReplenishInfoEProds",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplenishInfoEProds_Employees_EmployeeId",
                table: "ReplenishInfoEProds");

            migrationBuilder.DropIndex(
                name: "IX_ReplenishInfoEProds_EmployeeId",
                table: "ReplenishInfoEProds");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ReplenishInfoEProds");
        }
    }
}
