using System;
using System.Runtime.Serialization;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Grpc.Models;

[DataContract]
public class GetFireblockFeeResponse
{
    [DataMember(Order = 1)] public FeeFireblock FeeFireblock { get; set; }
}