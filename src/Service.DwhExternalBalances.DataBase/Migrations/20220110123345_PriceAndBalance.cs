using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class PriceAndBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConvertPrice",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BaseAsset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    QuotedAsset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertPrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExternalBalance",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Asset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketPrice",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrokerId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    InstrumentSymbol = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InstrumentStatus = table.Column<int>(type: "int", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    SourceMarket = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketPrice", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConvertPrice_BaseAsset_QuotedAsset",
                schema: "data",
                table: "ConvertPrice",
                columns: new[] { "BaseAsset", "QuotedAsset" },
                unique: true,
                filter: "[BaseAsset] IS NOT NULL AND [QuotedAsset] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertPrice_Error",
                schema: "data",
                table: "ConvertPrice",
                column: "Error");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertPrice_UpdateDate",
                schema: "data",
                table: "ConvertPrice",
                column: "UpdateDate");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBalance_Asset",
                schema: "data",
                table: "ExternalBalance",
                column: "Asset");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBalance_BalanceDate",
                schema: "data",
                table: "ExternalBalance",
                column: "BalanceDate");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBalance_Exchange",
                schema: "data",
                table: "ExternalBalance",
                column: "Exchange");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBalance_Exchange_Asset_BalanceDate",
                schema: "data",
                table: "ExternalBalance",
                columns: new[] { "Exchange", "Asset", "BalanceDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrice_DateTime",
                schema: "data",
                table: "MarketPrice",
                column: "DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrice_InstrumentStatus",
                schema: "data",
                table: "MarketPrice",
                column: "InstrumentStatus");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrice_InstrumentSymbol",
                schema: "data",
                table: "MarketPrice",
                column: "InstrumentSymbol");

            migrationBuilder.CreateIndex(
                name: "IX_MarketPrice_Source_SourceMarket",
                schema: "data",
                table: "MarketPrice",
                columns: new[] { "Source", "SourceMarket" },
                unique: true,
                filter: "[Source] IS NOT NULL AND [SourceMarket] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvertPrice",
                schema: "data");

            migrationBuilder.DropTable(
                name: "ExternalBalance",
                schema: "data");

            migrationBuilder.DropTable(
                name: "MarketPrice",
                schema: "data");
        }
    }
}
