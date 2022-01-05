using System.Runtime.Serialization;

namespace Service.DwhExternalBalances.Domain.Models
{
    [DataContract]
    public class ExternalBalance
    {
        [DataMember(Order = 1)] public string Type { get; set; }
        [DataMember(Order = 2)] public string Name { get; set; }
        [DataMember(Order = 3)] public string Asset { get; set; }
        [DataMember(Order = 4)] public decimal Volume { get; set; }
    }
}