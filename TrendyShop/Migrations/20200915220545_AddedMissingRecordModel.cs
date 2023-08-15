using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedMissingRecordModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Missings",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    Cost = table.Column<float>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missings", x => new { x.Date, x.Cost, x.Name });
                    table.ForeignKey(
                        name: "FK_Missings_DatesOfClosings_Date",
                        column: x => x.Date,
                        principalTable: "DatesOfClosings",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Missings_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Missings_Cost_Name",
                table: "Missings",
                columns: new[] { "Cost", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Missings");
        }
    }
}
