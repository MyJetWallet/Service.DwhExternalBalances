using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class UpdateAssetsUsdPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetsUsdPrices",
                schema: "data",
                columns: table => new
                {
                    Asset = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsdPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetsUsdPrices", x => x.Asset);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsUsdPrices",
                schema: "data");
        }
    }
}
