using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.DwhExternalBalances.DataBase.Models;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.DataBase
{
    public class DwhContext : DbContext
    {
        public const string Schema = "data";
        private const string AllExternalBalancesTableName = "AllExternalBalances";
        private const string AllExternalBalancesHistoryTableName = "AllExternalBalancesHistory";
        private const string MarketPriceTableName = "MarketPrice";
        private const string ConvertIndexPriceTableName = "ConvertPrice";
        private const string ExternalBalanceTableName = "ExternalBalance";

        public static ILoggerFactory LoggerFactory { get; set; }

        public DbSet<ExternalBalance> ExternalBalances { get; set; }
        public DbSet<ExternalBalanceHistory> ExternalBalancesHistory { get; set; }
        public DbSet<MarketPriceEntity> MarketPrice { get; set; }
        public DbSet<ConvertIndexPriceEntity> ConvertPrice { get; set; }
        private DbSet<ExternalBalanceEntity> ExternalBalanceCollection { get; set; }
        

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


            modelBuilder.Entity<ExternalBalanceHistory>().ToTable(AllExternalBalancesHistoryTableName);
            modelBuilder.Entity<ExternalBalanceHistory>().HasKey(e => new { e.Type, e.Name, e.Asset, e.Timestemp });
            modelBuilder.Entity<ExternalBalanceHistory>().Property(e => e.Volume).HasPrecision(18,8);
            
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
    }
}