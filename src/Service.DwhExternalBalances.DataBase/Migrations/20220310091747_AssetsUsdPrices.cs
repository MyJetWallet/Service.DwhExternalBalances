using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class AssetsUsdPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllExternalBalancesHistory",
                schema: "data");

            migrationBuilder.CreateTable(
                name: "AssetsUsdPricesDailySnapShot",
                schema: "data",
                columns: table => new
                {
                    Asset = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "date", nullable: false),
                    UsdPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsUsdPricesDailySnapShot", x => new { x.Asset, x.UpdateDate });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsUsdPricesDailySnapShot",
                schema: "data");

            migrationBuilder.CreateTable(
                name: "AllExternalBalancesHistory",
                schema: "data",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Asset = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestemp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllExternalBalancesHistory", x => new { x.Type, x.Name, x.Asset, x.Timestemp });
                });
        }
    }
}
