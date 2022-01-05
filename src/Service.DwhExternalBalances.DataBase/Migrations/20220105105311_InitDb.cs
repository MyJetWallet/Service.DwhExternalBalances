using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class InitDb : Migration
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
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Asset = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllExternalBalances", x => new { x.Type, x.Name, x.Asset });
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
                    Volume = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllExternalBalancesHistory", x => new { x.Type, x.Name, x.Asset, x.Timestemp });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllExternalBalances",
                schema: "data");

            migrationBuilder.DropTable(
                name: "AllExternalBalancesHistory",
                schema: "data");
        }
    }
}
