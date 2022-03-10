using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Fireblocks.Domain.Models.TransactionHistories;
using Service.AssetsDictionary.Domain.Models;
using Service.DwhExternalBalances.DataBase.Models;
using Service.DwhExternalBalances.Domain.Models;
using Service.IndexPrices.Domain.Models;

namespace Service.DwhExternalBalances.DataBase
{
    public class DwhContext : DbContext
    {
        public const string Schema = "data";
        private const string AllExternalBalancesTableName = "AllExternalBalances";
        private const string MarketPriceTableName = "MarketPrice";
        private const string ConvertIndexPriceTableName = "ConvertPrice";
        private const string ExternalBalanceTableName = "ExternalBalance";
        private const string TransactionFireBlocksTableName = "TransactionFireBlocks";
        private const string AssetsDictionaryTableName = "AssetsDictionary";
        private const string SpotInstrumentsTableName = "SpotInstrument";
        private const string MarketReferenceTableName = "MarketReference";
        private const string IndexPriceTableName = "AssetsUsdPrices";
        private const string IndexPriceShapShotTableName = "AssetsUsdPricesDailySnapShot";

        public static ILoggerFactory LoggerFactory { get; set; }
        public DbSet<ExternalBalance> ExternalBalances { get; set; }
        public DbSet<MarketPriceEntity> MarketPrice { get; set; }
        public DbSet<ConvertIndexPriceEntity> ConvertPrice { get; set; }
        
        public DbSet<ExternalBalanceEntity> ExternalBalanceCollection { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<SpotInstrument> SpotInstruments { get; set; }
        public DbSet<MarketReference> MarketReferences { get; set; }
        public DbSet<IndexPricesEntity> IndexPrices { get; set; }
        
        public DbSet<IndexPrice> IndexPricesShanpshot { get; set; }
        

        public DwhContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (LoggerFactory != null)
            {
                optionsBuilder.UseLoggerFactory(LoggerFactory).EnableSensitiveDataLogging();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(Schema);

            modelBuilder.Entity<ExternalBalance>().ToTable(AllExternalBalancesTableName);
            modelBuilder.Entity<ExternalBalance>().HasNoKey();
            modelBuilder.Entity<ExternalBalance>().Property(e => e.Volume).HasPrecision(18, 8);
            modelBuilder.Entity<ExternalBalance>().Property(e => e.AssetNetwork).IsRequired(false);
            modelBuilder.Entity<ExternalBalance>().Property(e => e.Name).IsRequired(false);
            
            modelBuilder.Entity<MarketPriceEntity>().ToTable(MarketPriceTableName);
            modelBuilder.Entity<MarketPriceEntity>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<MarketPriceEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<MarketPriceEntity>().Property(e => e.BrokerId).HasMaxLength(64);
            modelBuilder.Entity<MarketPriceEntity>().Property(e => e.InstrumentSymbol).HasMaxLength(64);
            modelBuilder.Entity<MarketPriceEntity>().Property(e => e.Source).HasMaxLength(64);
            modelBuilder.Entity<MarketPriceEntity>().Property(e => e.SourceMarket).HasMaxLength(64);
            modelBuilder.Entity<MarketPriceEntity>().HasIndex(e => new {e.Source, e.SourceMarket}).IsUnique();
            modelBuilder.Entity<MarketPriceEntity>().HasIndex(e => e.DateTime);
            modelBuilder.Entity<MarketPriceEntity>().HasIndex(e => e.InstrumentStatus);
            modelBuilder.Entity<MarketPriceEntity>().HasIndex(e => e.InstrumentSymbol);
            
            modelBuilder.Entity<ConvertIndexPriceEntity>().ToTable(ConvertIndexPriceTableName);
            modelBuilder.Entity<ConvertIndexPriceEntity>().Property(e => e.BaseAsset).HasMaxLength(64);
            modelBuilder.Entity<ConvertIndexPriceEntity>().Property(e => e.QuotedAsset).HasMaxLength(64);
            modelBuilder.Entity<ConvertIndexPriceEntity>().Property(e => e.Error).HasMaxLength(256);
            modelBuilder.Entity<ConvertIndexPriceEntity>().HasKey(e => new {e.BaseAsset, e.QuotedAsset});
            modelBuilder.Entity<ConvertIndexPriceEntity>().HasIndex(e => e.UpdateDate);
            modelBuilder.Entity<ConvertIndexPriceEntity>().HasIndex(e => e.Error);
            
            modelBuilder.Entity<ExternalBalanceEntity>().ToTable(ExternalBalanceTableName);
            modelBuilder.Entity<ExternalBalanceEntity>().Property(e => e.Id).UseIdentityColumn();
            modelBuilder.Entity<ExternalBalanceEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<ExternalBalanceEntity>().Property(e => e.Asset).HasMaxLength(64);
            modelBuilder.Entity<ExternalBalanceEntity>().Property(e => e.Exchange).HasMaxLength(64);
            modelBuilder.Entity<ExternalBalanceEntity>().HasIndex(e => new {e.Exchange, e.Asset, e.BalanceDate}).IsUnique();
            modelBuilder.Entity<ExternalBalanceEntity>().HasIndex(e => e.Exchange);
            modelBuilder.Entity<ExternalBalanceEntity>().HasIndex(e => e.Asset);
            modelBuilder.Entity<ExternalBalanceEntity>().HasIndex(e => e.BalanceDate);

            modelBuilder.Entity<TransactionHistory>().ToTable(TransactionFireBlocksTableName);
            modelBuilder.Entity<TransactionHistory>().HasKey(e => new {e.TxHash, e.FireblocksAssetId});
            modelBuilder.Entity<TransactionHistory>().HasIndex(e => e.UpdatedDateUnix);
            modelBuilder.Entity<TransactionHistory>().OwnsOne(e => e.Source);
            modelBuilder.Entity<TransactionHistory>().OwnsOne(e => e.Destination);

            modelBuilder.Entity<Asset>().ToTable(AssetsDictionaryTableName);
            modelBuilder.Entity<Asset>().HasNoKey();
            modelBuilder.Entity<Asset>().Ignore(e => e.DepositBlockchains);
            modelBuilder.Entity<Asset>().Ignore(e => e.WithdrawalBlockchains);

            modelBuilder.Entity<SpotInstrument>().ToTable(SpotInstrumentsTableName);
            modelBuilder.Entity<SpotInstrument>().HasNoKey();

            modelBuilder.Entity<MarketReference>().ToTable(MarketReferenceTableName);
            modelBuilder.Entity<MarketReference>().HasNoKey();

            modelBuilder.Entity<IndexPricesEntity>().ToTable(IndexPriceTableName);
            modelBuilder.Entity<IndexPricesEntity>().HasKey(e => e.Asset);

            modelBuilder.Entity<IndexPrice>().ToTable(IndexPriceShapShotTableName);
            modelBuilder.Entity<IndexPrice>().Property(e => e.UpdateDate).HasColumnType("date");
            modelBuilder.Entity<IndexPrice>().HasKey(e => new { e.Asset, e.UpdateDate});
        }
        
        public async Task UpsertMarketPrice(IEnumerable<MarketPriceEntity> prices)
        {
            await MarketPrice
                .UpsertRange(prices)
                .On(e => new {e.Source, e.SourceMarket})
                .RunAsync();
        }
        
        public async Task UpsertConvertPrice(IEnumerable<ConvertIndexPriceEntity> prices)
        {
            await ConvertPrice
                .UpsertRange(prices)
                .On(e => new {e.BaseAsset, e.QuotedAsset})
                .RunAsync();
        }

        public async Task SinglUpsertConvertPrice(ConvertIndexPriceEntity item)
        {
            await ConvertPrice
                .UpsertRange(item)
                .On(e => new {e.BaseAsset, e.QuotedAsset})
                .RunAsync();
        }
        
        public async Task UpsertExternalBalances(IEnumerable<ExternalBalance> allBalances)
        {
            await ExternalBalances.UpsertRange(allBalances)
                .On(e => new { e.Type, e.Name, e.Asset, e.AssetNetwork })
                .RunAsync();
        }

        public async Task UpsertAssetDictionary(IEnumerable<Asset> assets)
        {
            await Assets.UpsertRange(assets)
                .On(e => new { e.BrokerId, e.Symbol })
                .RunAsync();
        }

        public async Task UpsertSpotInstruments(IEnumerable<SpotInstrument> spotInstruments)
        {
            await SpotInstruments.UpsertRange(spotInstruments)
                .On(e => new { e.BrokerId, e.Symbol })
                .RunAsync();
        }

        public async Task UpsertMarketReference(IEnumerable<MarketReference> marketReferences)
        {
            await MarketReferences.UpsertRange(marketReferences)
                .On(e => new { e.BrokerId, e.Id })
                .RunAsync();
        }

        public async Task UpdateIndexPrices(IEnumerable<IndexPricesEntity> indexPrices)
        {
            await IndexPrices.UpsertRange(indexPrices)
                .On(e => e.Asset)
                .RunAsync();
        }

        public async Task UpdateIndexPriceShapshot(IEnumerable<IndexPrice> indexPrices)
        {
            await IndexPricesShanpshot.UpsertRange(indexPrices)
                .On(e => new { e.Asset, e.UpdateDate })
                .RunAsync();
        }
    }
}