using MyJetWallet.Fireblocks.Domain.Models.TransactionHistories;

namespace Service.DwhExternalBalances.DataBase.Models;

public class TransactionHistoryEntity : TransactionHistory
{
    public Decimal AssetIndexPrice { get; set; }
    
    public Decimal FeeAssetIndexPrice { get; set; }

    public TransactionHistoryEntity()
    {
    }

    public TransactionHistoryEntity(TransactionHistory transaction)
    {
        this.Id = transaction.Id;
        this.TxHash = transaction.TxHash;
        this.CreatedDateUnix = transaction.CreatedDateUnix;
        this.UpdatedDateUnix = transaction.UpdatedDateUnix;
        this.FireblocksAssetId = transaction.FireblocksAssetId;
        this.FireblocksFeeAssetId = transaction.FireblocksFeeAssetId;
        this.Amount = transaction.Amount;
        this.Fee = transaction.Fee;
        this.Status = transaction.Status;
        this.SourceAddress = transaction.SourceAddress;
        this.DestinationAddress = transaction.DestinationAddress;
        this.Source = transaction.Source;
        this.Destination = transaction.Destination;
        this.AssetSymbol = transaction.AssetSymbol;
        this.AssetNetwork = transaction.AssetNetwork;
        this.FeeAssetSymbol = transaction.FeeAssetSymbol;
        this.FeeAssetNetwork = transaction.FeeAssetNetwork;
    }
}