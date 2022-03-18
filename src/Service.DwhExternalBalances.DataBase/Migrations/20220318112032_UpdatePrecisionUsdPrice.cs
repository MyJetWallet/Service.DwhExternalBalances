using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class UpdatePrecisionUsdPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UsdPrice",
                schema: "data",
                table: "AssetsUsdPricesDailySnapShot",
                type: "decimal(18,10)",
                precision: 18,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "UsdPrice",
                schema: "data",
                table: "AssetsUsdPrices",
                type: "decimal(18,10)",
                precision: 18,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "UsdPrice",
                schema: "data",
                table: "AssetsUsdPricesDailySnapShot",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)",
                oldPrecision: 18,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "UsdPrice",
                schema: "data",
                table: "AssetsUsdPrices",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,10)",
                oldPrecision: 18,
                oldScale: 10);
        }
    }
}
