using Microsoft.EntityFrameworkCore.Migrations;

namespace TrendyShop.Migrations
{
    public partial class UpdatedExpendableProductKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_ExpProductId",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables");

            migrationBuilder.DropIndex(
                name: "IX_PackExpedables_ExpProductId",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "ExpProductId",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "ExpendableProductId",
                table: "ExpendableProducts");

            migrationBuilder.AddColumn<float>(
                name: "Cost",
                table: "PackExpedables",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Cost",
                table: "ExpendableProducts",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables",
                column: "Cost");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts",
                column: "Cost");

            migrationBuilder.AddForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost",
                table: "PackExpedables",
                column: "Cost",
                principalTable: "ExpendableProducts",
                principalColumn: "Cost",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_Cost",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "PackExpedables");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ExpendableProducts");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "PackExpedables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpProductId",
                table: "PackExpedables",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExpendableProductId",
                table: "ExpendableProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackExpedables",
                table: "PackExpedables",
                columns: new[] { "PackageId", "ExpProductId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpendableProducts",
                table: "ExpendableProducts",
                column: "ExpendableProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PackExpedables_ExpProductId",
                table: "PackExpedables",
                column: "ExpProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackExpedables_ExpendableProducts_ExpProductId",
                table: "PackExpedables",
                column: "ExpProductId",
                principalTable: "ExpendableProducts",
                principalColumn: "ExpendableProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
