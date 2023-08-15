using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class DailyClosing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatesOfClosings",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatesOfClosings", x => x.Date);
                });

            migrationBuilder.CreateTable(
                name: "DailyClosings",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AmountInStorage = table.Column<float>(nullable: false),
                    Room1 = table.Column<float>(nullable: false),
                    Room2 = table.Column<float>(nullable: false),
                    Room3 = table.Column<float>(nullable: false),
                    TotalAmount = table.Column<float>(nullable: false),
                    ConsuptionSincePreviuosClosing = table.Column<float>(nullable: false),
                    PreviousClousing = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyClosings", x => new { x.Date, x.Name, x.Cost });
                    table.ForeignKey(
                        name: "FK_DailyClosings_DatesOfClosings_Date",
                        column: x => x.Date,
                        principalTable: "DatesOfClosings",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyClosings_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyClosings_Cost_Name",
                table: "DailyClosings",
                columns: new[] { "Cost", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyClosings");

            migrationBuilder.DropTable(
                name: "DatesOfClosings");
        }
    }
}
