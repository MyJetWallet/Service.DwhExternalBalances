using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class DeleteExternalBalanceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalBalance",
                schema: "data");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExternalBalance",
                schema: "data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Exchange = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalBalance", x => x.Id);
                });

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
        }
    }
}
