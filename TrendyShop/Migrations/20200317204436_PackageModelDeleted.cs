using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class PackageModelDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lodgings_Packages_PackageId",
                table: "Lodgings");

            migrationBuilder.DropForeignKey(
                name: "FK_PackExpedables_Packages_PackageId",
                table: "PackExpedables");

            migrationBuilder.DropForeignKey(
                name: "FK_PackGastronomics_Packages_PackageId",
                table: "PackGastronomics");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics");

            migrationBuilder.DropIndex(
                name: "IX_PackGastronomics_PackageId",
                table: "PackGastronomics");

            migrationBuilder.DropIndex(
                name: "IX_Lodgings_PackageId",
                table: "Lodgings");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "PackGastronomics");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Lodgings");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics",
                columns: new[] { "Cost", "Name" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "PackGastronomics",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Lodgings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackGastronomics",
                table: "PackGastronomics",
                columns: new[] { "Cost", "Name", "PackageId" });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    PackageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.PackageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackGastronomics_PackageId",
                table: "PackGastronomics",
                column: "PackageId");

            migrationBuilder.CreateIndex(
                name: "IX_Lodgings_PackageId",
                table: "Lodgings",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lodgings_Packages_PackageId",
                table: "Lodgings",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackExpedables_Packages_PackageId",
                table: "PackExpedables",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PackGastronomics_Packages_PackageId",
                table: "PackGastronomics",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "PackageId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
