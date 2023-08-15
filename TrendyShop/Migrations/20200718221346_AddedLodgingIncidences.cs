using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedLodgingIncidences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidences_Lodgings_LodgingRoomId_LodgingDate",
                table: "Incidences");

            migrationBuilder.DropIndex(
                name: "IX_Incidences_LodgingRoomId_LodgingDate",
                table: "Incidences");

            migrationBuilder.DropColumn(
                name: "LodgingDate",
                table: "Incidences");

            migrationBuilder.DropColumn(
                name: "LodgingRoomId",
                table: "Incidences");

            migrationBuilder.CreateTable(
                name: "LodgingIncidences",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IncidenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LodgingIncidences", x => new { x.RoomId, x.Date, x.IncidenceId });
                    table.ForeignKey(
                        name: "FK_LodgingIncidences_Incidences_IncidenceId",
                        column: x => x.IncidenceId,
                        principalTable: "Incidences",
                        principalColumn: "IncidenceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LodgingIncidences_Lodgings_RoomId_Date",
                        columns: x => new { x.RoomId, x.Date },
                        principalTable: "Lodgings",
                        principalColumns: new[] { "RoomId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LodgingIncidences_IncidenceId",
                table: "LodgingIncidences",
                column: "IncidenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LodgingIncidences");

            migrationBuilder.AddColumn<DateTime>(
                name: "LodgingDate",
                table: "Incidences",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LodgingRoomId",
                table: "Incidences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Incidences_LodgingRoomId_LodgingDate",
                table: "Incidences",
                columns: new[] { "LodgingRoomId", "LodgingDate" });

            migrationBuilder.AddForeignKey(
                name: "FK_Incidences_Lodgings_LodgingRoomId_LodgingDate",
                table: "Incidences",
                columns: new[] { "LodgingRoomId", "LodgingDate" },
                principalTable: "Lodgings",
                principalColumns: new[] { "RoomId", "Date" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
