using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class IndexPriceFireblocksTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AssetIndexPrice",
                schema: "data",
                table: "TransactionFireBlocks",
                type: "decimal(18,10)",
                precision: 18,
                scale: 10,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeAssetIndexPrice",
                schema: "data",
                table: "TransactionFireBlocks",
                type: "decimal(18,10)",
                precision: 18,
                scale: 10,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssetIndexPrice",
                schema: "data",
                table: "TransactionFireBlocks");

            migrationBuilder.DropColumn(
                name: "FeeAssetIndexPrice",
                schema: "data",
                table: "TransactionFireBlocks");
        }
    }
}
