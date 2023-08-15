using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AboutRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidences_Lodgings_LodgingRoomId_LodgingDate",
                table: "Incidences");

            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Reservations_RoomId_Date",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasePerLodgings_Lodgings_RoomId_Date",
                table: "PurchasePerLodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomProducts_Rooms_RoomId",
                table: "RoomProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts");

            migrationBuilder.DropIndex(
                name: "IX_RoomProducts_RoomId",
                table: "RoomProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings");

            migrationBuilder.DropIndex(
                name: "IX_PurchasePerLodgings_RoomId_Date",
                table: "PurchasePerLodgings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lodgings",
                table: "Lodgings");

            migrationBuilder.DropIndex(
                name: "IX_Incidences_LodgingRoomId_LodgingDate",
                table: "Incidences");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomProducts");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "PurchasePerLodgings");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "LodgingRoomId",
                table: "Incidences");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts",
                columns: new[] { "Name", "Cost" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings",
                columns: new[] { "Name", "Cost", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lodgings",
                table: "Lodgings",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_Incidences_LodgingDate",
                table: "Incidences",
                column: "LodgingDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Incidences_Lodgings_LodgingDate",
                table: "Incidences",
                column: "LodgingDate",
                principalTable: "Lodgings",
                principalColumn: "Date",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Incidences_Lodgings_LodgingDate",
                table: "Incidences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lodgings",
                table: "Lodgings");

            migrationBuilder.DropIndex(
                name: "IX_Incidences_LodgingDate",
                table: "Incidences");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "PurchasePerLodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Lodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LodgingRoomId",
                table: "Incidences",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomProducts",
                table: "RoomProducts",
                columns: new[] { "Name", "Cost", "RoomId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "RoomId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasePerLodgings",
                table: "PurchasePerLodgings",
                columns: new[] { "Name", "Cost", "RoomId", "Date" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lodgings",
                table: "Lodgings",
                columns: new[] { "RoomId", "Date" });

            migrationBuilder.CreateIndex(
                name: "IX_RoomProducts_RoomId",
                table: "RoomProducts",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasePerLodgings_RoomId_Date",
                table: "PurchasePerLodgings",
                columns: new[] { "RoomId", "Date" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Reservations_RoomId_Date",
                table: "Lodgings",
                columns: new[] { "RoomId", "Date" },
                principalTable: "Reservations",
                principalColumns: new[] { "RoomId", "Date" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasePerLodgings_Lodgings_RoomId_Date",
                table: "PurchasePerLodgings",
                columns: new[] { "RoomId", "Date" },
                principalTable: "Lodgings",
                principalColumns: new[] { "RoomId", "Date" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomProducts_Rooms_RoomId",
                table: "RoomProducts",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
