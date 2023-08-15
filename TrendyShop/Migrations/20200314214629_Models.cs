using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    IsBlocked = table.Column<bool>(nullable: false),
                    LastEntrance = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "ExpendableProducts",
                columns: table => new
                {
                    ExpendableProductId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpendableProducts", x => x.ExpendableProductId);
                });

            migrationBuilder.CreateTable(
                name: "GastronomicProducts",
                columns: table => new
                {
                    Cost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GastronomicProducts", x => new { x.Cost, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Maintenances",
                columns: table => new
                {
                    MaintenanceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenances", x => x.MaintenanceId);
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomNumber = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomNumber);
                });

            migrationBuilder.CreateTable(
                name: "PackExpedables",
                columns: table => new
                {
                    PackageId = table.Column<int>(nullable: false),
                    ExpProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackExpedables", x => new { x.PackageId, x.ExpProductId });
                    table.ForeignKey(
                        name: "FK_PackExpedables_ExpendableProducts_ExpProductId",
                        column: x => x.ExpProductId,
                        principalTable: "ExpendableProducts",
                        principalColumn: "ExpendableProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackExpedables_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PackGastronomics",
                columns: table => new
                {
                    Cost = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackGastronomics", x => new { x.Cost, x.Name, x.PackageId });
                    table.ForeignKey(
                        name: "FK_PackGastronomics_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PackGastronomics_GastronomicProducts_Cost_Name",
                        columns: x => new { x.Cost, x.Name },
                        principalTable: "GastronomicProducts",
                        principalColumns: new[] { "Cost", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Customer = table.Column<string>(nullable: true),
                    StandardPrice = table.Column<int>(nullable: false),
                    Romantic = table.Column<bool>(nullable: false),
                    Companion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => new { x.RoomId, x.Date });
                    table.ForeignKey(
                        name: "FK_Reservations_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true),
                    PackageId = table.Column<int>(nullable: false),
                    CustomerId = table.Column<string>(nullable: true),
                    ExtraCharge = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    Prepaid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => new { x.RoomId, x.Date });
                    table.ForeignKey(
                        name: "FK_Lodgings_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lodgings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lodgings_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lodgings_Reservations_RoomId_Date",
                        columns: x => new { x.RoomId, x.Date },
                        principalTable: "Reservations",
                        principalColumns: new[] { "RoomId", "Date" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Incidences",
                columns: table => new
                {
                    IncidenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    LodgingDate = table.Column<DateTime>(nullable: true),
                    LodgingRoomId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidences", x => x.IncidenceId);
                    table.ForeignKey(
                        name: "FK_Incidences_Lodgings_LodgingRoomId_LodgingDate",
                        columns: x => new { x.LodgingRoomId, x.LodgingDate },
                        principalTable: "Lodgings",
                        principalColumns: new[] { "RoomId", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidences_LodgingRoomId_LodgingDate",
                table: "Incidences",
                columns: new[] { "LodgingRoomId", "LodgingDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_CustomerId",
                table: "Lodgings",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_EmployeeId",
                table: "Lodgings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_PackageId",
                table: "Lodgings",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_PackExpedables_ExpProductId",
                table: "PackExpedables",
                column: "ExpProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PackGastronomics_PackageId",
                table: "PackGastronomics",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidences");

            migrationBuilder.DropTable(
                name: "Maintenances");

            migrationBuilder.DropTable(
                name: "PackExpedables");

            migrationBuilder.DropTable(
                name: "PackGastronomics");

            migrationBuilder.DropTable(
                name: "Lodgings");

            migrationBuilder.DropTable(
                name: "ExpendableProducts");

            migrationBuilder.DropTable(
                name: "GastronomicProducts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
