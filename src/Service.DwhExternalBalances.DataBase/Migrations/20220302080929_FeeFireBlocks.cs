using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class FeeFireBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TransferPeerPath",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferPeerPath", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeeTransferFireBlocks",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TxHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateUnix = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDateUnix = table.Column<long>(type: "bigint", nullable: false),
                    FireblocksAssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FireblocksFeeAssetId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    SourceAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DestinationAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SourceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DestinationId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssetSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssetNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeAssetSymbol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeAssetNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FeeTransferFireBlocks_TransferPeerPath_DestinationId",
                        column: x => x.DestinationId,
                        principalSchema: "data",
                        principalTable: "TransferPeerPath",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeeTransferFireBlocks_TransferPeerPath_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "data",
                        principalTable: "TransferPeerPath",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeTransferFireBlocks_DestinationId",
                schema: "data",
                table: "FeeTransferFireBlocks",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeTransferFireBlocks_SourceId",
                schema: "data",
                table: "FeeTransferFireBlocks",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeeTransferFireBlocks",
                schema: "data");

            migrationBuilder.DropTable(
                name: "TransferPeerPath",
                schema: "data");
        }
    }
}
