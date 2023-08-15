using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class Inventary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchasePerLodgings",
                columns: table => new
                {
                    Cost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchasePerLodgings", x => new { x.Name, x.Cost, x.RoomId, x.Date });
                    table.ForeignKey(
                        name: "FK_PurchasePerLodgings_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchasePerLodgings_Lodgings_RoomId_Date",
                        columns: x => new { x.RoomId, x.Date },
                        principalTable: "Lodgings",
                        principalColumns: new[] { "RoomId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomProducts",
                columns: table => new
                {
                    Cost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomProducts", x => new { x.Name, x.Cost, x.RoomId });
                    table.ForeignKey(
                        name: "FK_RoomProducts_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomProducts_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Cost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AmountAvailable = table.Column<int>(nullable: false),
                    AmountInRooms = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => new { x.Name, x.Cost });
                    table.ForeignKey(
                        name: "FK_Storage_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePerLodgings_Cost_Name",
                table: "PurchasePerLodgings",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePerLodgings_RoomId_Date",
                table: "PurchasePerLodgings",
                columns: new[] { "RoomId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomProducts_RoomId",
                table: "RoomProducts",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomProducts_Cost_Name",
                table: "RoomProducts",
                columns: new[] { "Cost", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Storage_Cost_Name",
                table: "Storage",
                columns: new[] { "Cost", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasePerLodgings");

            migrationBuilder.DropTable(
                name: "RoomProducts");

            migrationBuilder.DropTable(
                name: "Storage");
        }
    }
}
