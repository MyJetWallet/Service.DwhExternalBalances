using System;
using System.Runtime.Serialization;

namespace Service.DwhExternalBalances.Grpc.Models;

[DataContract]
public class GetByDateRequest
{
    [DataMember(Order = 1)]public DateTime DateFrom { get; set; }
    [DataMember(Order = 2)]public DateTime DateTo { get; set; }
}