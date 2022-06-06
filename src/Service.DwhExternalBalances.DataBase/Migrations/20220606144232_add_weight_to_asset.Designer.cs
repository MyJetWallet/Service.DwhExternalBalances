﻿// <auto-generated />
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
    [Migration("20220606144232_add_weight_to_asset")]
    partial class add_weight_to_asset
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("data")
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Service.AssetsDictionary.Domain.Models.Asset", b =>
                {
                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("BrokerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CanBeBaseAsset")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HideInTerminal")
                        .HasColumnType("bit");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMainNet")
                        .HasColumnType("bit");

                    b.Property<bool>("KycRequiredForDeposit")
                        .HasColumnType("bit");

                    b.Property<bool>("KycRequiredForTrade")
                        .HasColumnType("bit");

                    b.Property<bool>("KycRequiredForWithdrawal")
                        .HasColumnType("bit");

                    b.Property<string>("MatchingEngineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaxTradeValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinTradeValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PrefixSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.ToTable("AssetsDictionary", "data");
                });

            modelBuilder.Entity("Service.AssetsDictionary.Domain.Models.MarketReference", b =>
                {
                    b.Property<string>("AssociateAsset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssociateAssetPair")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrokerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMainNet")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartMarketTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.ToTable("MarketReference", "data");
                });

            modelBuilder.Entity("Service.AssetsDictionary.Domain.Models.SpotInstrument", b =>
                {
                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("BaseAsset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BrokerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConvertSourceExchange")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConvertSourceMarket")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("KycRequiredForTrade")
                        .HasColumnType("bit");

                    b.Property<decimal>("MarketOrderPriceThreshold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MatchingEngineId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MaxOppositeVolume")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MaxVolume")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("MinVolume")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("QuoteAsset")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.ToTable("SpotInstrument", "data");
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

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.IndexPricesEntity", b =>
                {
                    b.Property<string>("Asset")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("UsdPrice")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.HasKey("Asset");

                    b.ToTable("AssetsUsdPrices", "data");
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

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.TransactionHistoryEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<decimal>("AssetIndexPrice")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<string>("AssetNetwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssetSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CreatedDateUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("DestinationAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Fee")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<decimal>("FeeAssetIndexPrice")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.Property<string>("FeeAssetNetwork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FeeAssetSymbol")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FireblocksAssetId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FireblocksFeeAssetId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TxHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UpdatedDateUnix")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedDateUnix");

                    b.ToTable("TransactionFireBlocks", "data");
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

            modelBuilder.Entity("Service.Fireblocks.Api.Grpc.Models.GasStation.GasStationBalance", b =>
                {
                    b.Property<string>("FireblocksAssetId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Balance")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.HasKey("FireblocksAssetId");

                    b.ToTable("GasStationBalance", "data");
                });

            modelBuilder.Entity("Service.IndexPrices.Domain.Models.IndexPrice", b =>
                {
                    b.Property<string>("Asset")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("date");

                    b.Property<decimal>("UsdPrice")
                        .HasPrecision(18, 10)
                        .HasColumnType("decimal(18,10)");

                    b.HasKey("Asset", "UpdateDate");

                    b.ToTable("AssetsUsdPricesDailySnapShot", "data");
                });

            modelBuilder.Entity("Service.DwhExternalBalances.DataBase.Models.TransactionHistoryEntity", b =>
                {
                    b.OwnsOne("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransferPeerPath", "Destination", b1 =>
                        {
                            b1.Property<string>("TransactionHistoryEntityId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TransactionHistoryEntityId");

                            b1.ToTable("TransactionFireBlocks", "data");

                            b1.WithOwner()
                                .HasForeignKey("TransactionHistoryEntityId");
                        });

                    b.OwnsOne("MyJetWallet.Fireblocks.Domain.Models.TransactionHistories.TransferPeerPath", "Source", b1 =>
                        {
                            b1.Property<string>("TransactionHistoryEntityId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Id")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("TransactionHistoryEntityId");

                            b1.ToTable("TransactionFireBlocks", "data");

                            b1.WithOwner()
                                .HasForeignKey("TransactionHistoryEntityId");
                        });

                    b.Navigation("Destination");

                    b.Navigation("Source");
                });
#pragma warning restore 612, 618
        }
    }
}
