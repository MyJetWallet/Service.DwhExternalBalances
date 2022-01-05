using System.Collections.Generic;
using System.Runtime.Serialization;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Grpc.Models
{
    [DataContract]
    public class GetAllBalancesResponse
    {
        [DataMember(Order = 1)]
        public List<ExternalBalance> Balances { get; set; }
    }
}