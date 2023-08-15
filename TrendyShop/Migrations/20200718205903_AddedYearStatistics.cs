using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class AddedYearStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YearStatistics",
                columns: table => new
                {
                    Mes = table.Column<string>(nullable: true),
                    Reservas = table.Column<int>(nullable: false),
                    ReservasDobles = table.Column<int>(nullable: false),
                    ReservasDiarias = table.Column<float>(nullable: false),
                    IngresoPorRenta = table.Column<float>(nullable: false),
                    Consumo = table.Column<float>(nullable: false),
                    GananciaPorConsumo = table.Column<float>(nullable: false),
                    IngresoPorDaños = table.Column<float>(nullable: false),
                    IngresoBruto = table.Column<float>(nullable: false),
                    Salario = table.Column<float>(nullable: false),
                    OtrosGastos = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YearStatistics");
        }
    }
}
