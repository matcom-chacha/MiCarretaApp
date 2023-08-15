using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class RenamedReplenishInfoEProdTableTOExpendableProductOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplenishInfoEProds");

            migrationBuilder.CreateTable(
                name: "ExpendableProductOperations",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AlreadyInStorageAmount = table.Column<float>(nullable: false),
                    OperationAmount = table.Column<float>(nullable: false),
                    POTypeId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpendableProductOperations", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_ExpendableProductOperations_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExpendableProductOperations_ProductOperationTypes_POTypeId",
                        column: x => x.POTypeId,
                        principalTable: "ProductOperationTypes",
                        principalColumn: "POTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpendableProductOperations_ExpendableProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "ExpendableProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpendableProductOperations_EmployeeId",
                table: "ExpendableProductOperations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpendableProductOperations_POTypeId",
                table: "ExpendableProductOperations",
                column: "POTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpendableProductOperations");

            migrationBuilder.CreateTable(
                name: "ReplenishInfoEProds",
                columns: table => new
                {
                    Cost = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AlreadyInStorageAmount = table.Column<float>(type: "real", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    OperationAmount = table.Column<float>(type: "real", nullable: false),
                    POTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplenishInfoEProds", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_ReplenishInfoEProds_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplenishInfoEProds_ProductOperationTypes_POTypeId",
                        column: x => x.POTypeId,
                        principalTable: "ProductOperationTypes",
                        principalColumn: "POTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplenishInfoEProds_ExpendableProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "ExpendableProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoEProds_EmployeeId",
                table: "ReplenishInfoEProds",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoEProds_POTypeId",
                table: "ReplenishInfoEProds",
                column: "POTypeId");
        }
    }
}
