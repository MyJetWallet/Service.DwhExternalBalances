using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class Init_Db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "data");

            migrationBuilder.CreateTable(
                name: "AllExternalBalances",
                schema: "data",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Asset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volume = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    AssetNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

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

            migrationBuilder.CreateTable(
                name: "ConvertPrice",
                schema: "data",
                columns: table => new
                {
                    BaseAsset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    QuotedAsset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertPrice", x => new { x.BaseAsset, x.QuotedAsset });
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

            migrationBuilder.CreateTable(
                name: "TransactionFireBlocks",
                schema: "data",
                columns: table => new
                {
                    TxHash = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FireblocksAssetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateUnix = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDateUnix = table.Column<long>(type: "bigint", nullable: false),
                    FireblocksFeeAssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source_Type = table.Column<int>(type: "int", nullable: true),
                    Source_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination_Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Destination_Type = table.Column<int>(type: "int", nullable: true),
                    Destination_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeAssetSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeAssetNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionFireBlocks", x => new { x.TxHash, x.FireblocksAssetId });
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TransactionFireBlocks_UpdatedDateUnix",
                schema: "data",
                table: "TransactionFireBlocks",
                column: "UpdatedDateUnix");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllExternalBalances",
                schema: "data");

            migrationBuilder.DropTable(
                name: "AllExternalBalancesHistory",
                schema: "data");

            migrationBuilder.DropTable(
                name: "ConvertPrice",
                schema: "data");

            migrationBuilder.DropTable(
                name: "ExternalBalance",
                schema: "data");

            migrationBuilder.DropTable(
                name: "MarketPrice",
                schema: "data");

            migrationBuilder.DropTable(
                name: "TransactionFireBlocks",
                schema: "data");
        }
    }
}
