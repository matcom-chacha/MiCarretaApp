using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class RenamedReplenishInfoGprodtableForGastronomicProductOperations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplenishInfoGProds");

            migrationBuilder.CreateTable(
                name: "GastronomicProductOperations",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AmountAvailable = table.Column<float>(nullable: false),
                    AmountInrooms = table.Column<float>(nullable: false),
                    OperationAmount = table.Column<float>(nullable: false),
                    POTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastronomicProductOperations", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_GastronomicProductOperations_ProductOperationTypes_POTypeId",
                        column: x => x.POTypeId,
                        principalTable: "ProductOperationTypes",
                        principalColumn: "POTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GastronomicProductOperations_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GastronomicProductOperations_POTypeId",
                table: "GastronomicProductOperations",
                column: "POTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GastronomicProductOperations");

            migrationBuilder.CreateTable(
                name: "ReplenishInfoGProds",
                columns: table => new
                {
                    Cost = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountAvailable = table.Column<float>(type: "real", nullable: false),
                    AmountInrooms = table.Column<float>(type: "real", nullable: false),
                    OperationAmount = table.Column<float>(type: "real", nullable: false),
                    POTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplenishInfoGProds", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_ReplenishInfoGProds_ProductOperationTypes_POTypeId",
                        column: x => x.POTypeId,
                        principalTable: "ProductOperationTypes",
                        principalColumn: "POTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReplenishInfoGProds_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplenishInfoGProds_POTypeId",
                table: "ReplenishInfoGProds",
                column: "POTypeId");
        }
    }
}
