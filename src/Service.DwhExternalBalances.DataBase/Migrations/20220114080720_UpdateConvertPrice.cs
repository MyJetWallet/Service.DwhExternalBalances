using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    public partial class UpdateConvertPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvertPrice",
                schema: "data",
                table: "ConvertPrice");

            migrationBuilder.DropIndex(
                name: "IX_ConvertPrice_BaseAsset_QuotedAsset",
                schema: "data",
                table: "ConvertPrice");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "data",
                table: "ConvertPrice");

            migrationBuilder.AlterColumn<string>(
                name: "QuotedAsset",
                schema: "data",
                table: "ConvertPrice",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BaseAsset",
                schema: "data",
                table: "ConvertPrice",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvertPrice",
                schema: "data",
                table: "ConvertPrice",
                columns: new[] { "BaseAsset", "QuotedAsset" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConvertPrice",
                schema: "data",
                table: "ConvertPrice");

            migrationBuilder.AlterColumn<string>(
                name: "QuotedAsset",
                schema: "data",
                table: "ConvertPrice",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "BaseAsset",
                schema: "data",
                table: "ConvertPrice",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                schema: "data",
                table: "ConvertPrice",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConvertPrice",
                schema: "data",
                table: "ConvertPrice",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertPrice_BaseAsset_QuotedAsset",
                schema: "data",
                table: "ConvertPrice",
                columns: new[] { "BaseAsset", "QuotedAsset" },
                unique: true,
                filter: "[BaseAsset] IS NOT NULL AND [QuotedAsset] IS NOT NULL");
        }
    }
}
