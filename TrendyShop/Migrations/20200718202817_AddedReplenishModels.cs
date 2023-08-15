using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedReplenishModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReplenishInfoEProds",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AlreadyInStorageAmount = table.Column<float>(nullable: false),
                    ToAddAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplenishInfoEProds", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_ReplenishInfoEProds_ExpendableProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "ExpendableProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplenishInfoGProds",
                columns: table => new
                {
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    AmountAvailable = table.Column<float>(nullable: false),
                    AmountInrooms = table.Column<float>(nullable: false),
                    ToAddAmount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplenishInfoGProds", x => new { x.Cost, x.Name, x.Date });
                    table.ForeignKey(
                        name: "FK_ReplenishInfoGProds_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplenishInfoEProds");

            migrationBuilder.DropTable(
                name: "ReplenishInfoGProds");
        }
    }
}
