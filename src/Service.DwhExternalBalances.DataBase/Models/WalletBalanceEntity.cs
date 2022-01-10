using System;
using MyNoSqlServer.Abstractions;

namespace Service.DwhExternalBalances.DataBase.Models
{
    public class WalletBalanceEntity : MyNoSqlDbEntity
    {
        public const string TableName = "myjetwallet-bitgo-wallet-balance";

        public static string GeneratePartitionKey(string assetSymbol) => assetSymbol ?? "";

        public static string GenerateRowKey(string wallet) => wallet ?? "";

        public double Balance { get; set; }

        public string WalletId { get; set; }

        public string AssetSymbol { get; set; }

        public DateTime UpdateTime { get; set; }

        public static WalletBalanceEntity Create(
            double balance,
            string walletId,
            string assetSymbol,
            DateTime updateTime)
        {
            WalletBalanceEntity walletBalanceEntity = new WalletBalanceEntity();
            walletBalanceEntity.PartitionKey = WalletBalanceEntity.GeneratePartitionKey(assetSymbol);
            walletBalanceEntity.RowKey = WalletBalanceEntity.GenerateRowKey(walletId);
            walletBalanceEntity.AssetSymbol = assetSymbol;
            walletBalanceEntity.WalletId = walletId;
            walletBalanceEntity.Balance = balance;
            walletBalanceEntity.UpdateTime = updateTime;
            return walletBalanceEntity;
        }
    }
}