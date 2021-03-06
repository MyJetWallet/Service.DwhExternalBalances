// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Service.DwhExternalBalances.DataBase;

#nullable disable

namespace Service.DwhExternalBalances.DataBase.Migrations
{
    [DbContext(typeof(DwhContext))]
    [Migration("20220308103557_Init_Db")]
    partial class Init_Db
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("data")
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransactionHistory", b =>
                {
                    b.Property<string>("TxHash")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FireblocksAssetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("AssetNetwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedDateUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("DestinationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FeeAssetNetwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeeAssetSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FireblocksFeeAssetId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<long>("UpdatedDateUnix")
                        .HasColumnType("bigint");

                    b.HasKey("TxHash", "FireblocksAssetId");

                    b.HasIndex("UpdatedDateUnix");

                    b.ToTable("TransactionFireBlocks", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.ConvertIndexPriceEntity", b =>
                {
                    b.Property<string>("BaseAsset")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("QuotedAsset")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Error")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("BaseAsset", "QuotedAsset");

                    b.HasIndex("Error");

                    b.HasIndex("UpdateDate");

                    b.ToTable("ConvertPrice", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.ExternalBalanceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Asset")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("BalanceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Exchange")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Asset");

                    b.HasIndex("BalanceDate");

                    b.HasIndex("Exchange");

                    b.HasIndex("Exchange", "Asset", "BalanceDate")
                        .IsUnique();

                    b.ToTable("ExternalBalance", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.MarketPriceEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("BrokerId")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstrumentStatus")
                        .HasColumnType("int");

                    b.Property<string>("InstrumentSymbol")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Source")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("SourceMarket")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.HasKey("Id");

                    b.HasIndex("DateTime");

                    b.HasIndex("InstrumentStatus");

                    b.HasIndex("InstrumentSymbol");

                    b.HasIndex("Source", "SourceMarket")
                        .IsUnique()
                        .HasFilter("[Source] IS NOT NULL AND [SourceMarket] IS NOT NULL");

                    b.ToTable("MarketPrice", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.Domain.Models.ExternalBalance", b =>
                {
                    b.Property<string>("Asset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetNetwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Volume")
                        .HasPrecision(18, 8)
                        .HasColumnType("decimal(18,8)");

                    b.ToTable("AllExternalBalances", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.Domain.Models.ExternalBalanceHistory", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Asset")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Timestemp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Volume")
                        .HasPrecision(18, 8)
                        .HasColumnType("decimal(18,8)");

                    b.HasKey("Type", "Name", "Asset", "Timestemp");

                    b.ToTable("AllExternalBalancesHistory", "data");
                });

            modelBuilder.Entity("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransactionHistory", b =>
                {
                    b.OwnsOne("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransferPeerPath", "Destination", b1 =>
                        {
                            b1.Property<string>("TransactionHistoryTxHash")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("TransactionHistoryFireblocksAssetId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TransactionHistoryTxHash", "TransactionHistoryFireblocksAssetId");

                            b1.ToTable("TransactionFireBlocks", "data");

                            b1.WithOwner()
                                .HasForeignKey("TransactionHistoryTxHash", "TransactionHistoryFireblocksAssetId");
                        });

                    b.OwnsOne("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransferPeerPath", "Source", b1 =>
                        {
                            b1.Property<string>("TransactionHistoryTxHash")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("TransactionHistoryFireblocksAssetId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TransactionHistoryTxHash", "TransactionHistoryFireblocksAssetId");

                            b1.ToTable("TransactionFireBlocks", "data");

                            b1.WithOwner()
                                .HasForeignKey("TransactionHistoryTxHash", "TransactionHistoryFireblocksAssetId");
                        });

                    b.Navigation("Destination");

                    b.Navigation("Source");
                });
#pragma warning restore 612, 618
        }
    }
}
