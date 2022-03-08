using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class Dictionary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetsDictionary",
                schema: "data",
                columns: table => new
                {
                    BrokerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MatchingEngineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycRequiredForDeposit = table.Column<bool>(type: "bit", nullable: false),
                    KycRequiredForWithdrawal = table.Column<bool>(type: "bit", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrefixSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMainNet = table.Column<bool>(type: "bit", nullable: false),
                    CanBeBaseAsset = table.Column<bool>(type: "bit", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    KycRequiredForTrade = table.Column<bool>(type: "bit", nullable: false),
                    HideInTerminal = table.Column<bool>(type: "bit", nullable: false),
                    MinTradeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxTradeValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "MarketReference",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrokerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociateAsset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssociateAssetPair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    IsMainNet = table.Column<bool>(type: "bit", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartMarketTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "SpotInstrument",
                schema: "data",
                columns: table => new
                {
                    BrokerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseAsset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuoteAsset = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    MinVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxOppositeVolume = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarketOrderPriceThreshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MatchingEngineId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KycRequiredForTrade = table.Column<bool>(type: "bit", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertSourceExchange = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConvertSourceMarket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetsDictionary",
                schema: "data");

            migrationBuilder.DropTable(
                name: "MarketReference",
                schema: "data");

            migrationBuilder.DropTable(
                name: "SpotInstrument",
                schema: "data");
        }
    }
}
