using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Grpc.Models;

[DataContract]
public class GetFireblokTransactionsResponse
{
    [DataMember(Order = 1)]
    public List<FireblockTransaction> FireblockTransaction { get; set; }
}