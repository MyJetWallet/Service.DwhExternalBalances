using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class GasStationBalance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GasStationBalance",
                schema: "data",
                columns: table => new
                {
                    FireblocksAssetId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,18)", precision: 18, scale: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasStationBalance", x => x.FireblocksAssetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GasStationBalance",
                schema: "data");
        }
    }
}
