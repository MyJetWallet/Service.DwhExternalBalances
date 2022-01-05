using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.DataBase
{
    public class DwhContext : DbContext
    {
        public const string Schema = "data";
        public const string AllExternalBalancesTableName = "AllExternalBalances";
        public const string AllExternalBalancesHistoryTableName = "AllExternalBalancesHistory";

        public static ILoggerFactory LoggerFactory { get; set; }

        public DbSet<ExternalBalance> ExternalBalances { get; set; }
        public DbSet<ExternalBalanceHistory> ExternalBalancesHistory { get; set; }

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
            modelBuilder.Entity<ExternalBalance>().HasKey(e=>new{e.Type, e.Name, e.Asset});
            modelBuilder.Entity<ExternalBalance>().Property(e => e.Volume).HasPrecision(18, 8);


            modelBuilder.Entity<ExternalBalanceHistory>().ToTable(AllExternalBalancesHistoryTableName);
            modelBuilder.Entity<ExternalBalanceHistory>().HasKey(e => new { e.Type, e.Name, e.Asset, e.Timestemp });
            modelBuilder.Entity<ExternalBalanceHistory>().Property(e => e.Volume).HasPrecision(18,8);

        }
    }
}