using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class RenameAllPackageRelatedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackExpedables");

            migrationBuilder.DropTable(
                name: "PackGastronomics");

            migrationBuilder.CreateTable(
                name: "ExpendableProductStorage",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LastInsertionDate = table.Column<DateTime>(nullable: false),
                    LastInsertedQuantity = table.Column<float>(nullable: true),
                    TotalQuantity = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpendableProductStorage", x => new { x.Cost, x.Name });
                    table.ForeignKey(
                        name: "FK_ExpendableProductStorage_ExpendableProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "ExpendableProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => new { x.Cost, x.Name });
                    table.ForeignKey(
                        name: "FK_Package_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpendableProductStorage");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.CreateTable(
                name: "PackExpedables",
                columns: table => new
                {
                    Cost = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastInsertedQuantity = table.Column<float>(type: "real", nullable: true),
                    LastInsertionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalQuantity = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackExpedables", x => new { x.Cost, x.Name });
                    table.ForeignKey(
                        name: "FK_PackExpedables_ExpendableProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "ExpendableProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackGastronomics",
                columns: table => new
                {
                    Cost = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackGastronomics", x => new { x.Cost, x.Name });
                    table.ForeignKey(
                        name: "FK_PackGastronomics_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
