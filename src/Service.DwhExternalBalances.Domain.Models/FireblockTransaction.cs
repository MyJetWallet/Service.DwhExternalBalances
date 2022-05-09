using System;
using System.Runtime.Serialization;

namespace Service.DwhExternalBalances.Domain.Models
{
    public class FireblockTransaction
    {
        [DataMember(Order = 1)]public string TxHash { get; set; }
        [DataMember(Order = 2)]public string FireblocksAssetId { get; set; }
        [DataMember(Order = 3)]public string Id { get; set; }
        [DataMember(Order = 4)]public DateTime CreatedDate { get; set; }
        [DataMember(Order = 5)]public DateTime UpdatedDate { get; set; }
        [DataMember(Order = 6)]public string FireblocksFeeAssetId { get; set; }
        [DataMember(Order = 7)]public Decimal Amount { get; set; }
        [DataMember(Order = 8)]public Decimal Fee { get; set; }
        [DataMember(Order = 9)]public int Status { get; set; }
        [DataMember(Order = 10)]public string SourceAddress { get; set; }
        [DataMember(Order = 11)]public string DestinationAddress { get; set; }
        [DataMember(Order = 12)]public string SourceId { get; set; }
        [DataMember(Order = 13)]public int SourceType { get; set; }
        [DataMember(Order = 14)]public string SourceName { get; set; }
        [DataMember(Order = 15)]public int DestinationId { get; set; }
        [DataMember(Order = 16)]public string DestinationType { get; set; }
        [DataMember(Order = 17)]public string DestinationName { get; set; }
        [DataMember(Order = 18)]public string AssetSymbol { get; set; }
        [DataMember(Order = 19)]public string AssetNetwork { get; set; }
        [DataMember(Order = 20)]public string FeeAssetSymbol { get; set; }
        [DataMember(Order = 21)]public string FeeAssetNetwork { get; set; }
        [DataMember(Order = 22)]public Decimal AssetIndexPrice { get; set; }
        [DataMember(Order = 23)]public Decimal FeeAssetIndexPrice { get; set; }
        [DataMember(Order = 24)]public Decimal FeeUsd { get; set; }
    }
}