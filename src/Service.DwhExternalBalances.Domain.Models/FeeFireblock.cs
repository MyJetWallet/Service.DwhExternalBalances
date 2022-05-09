using System;
using System.Runtime.Serialization;

namespace Service.DwhExternalBalances.Domain.Models
{
    [DataContract]
    public class FeeFireblock
    {
        [DataMember(Order = 1)]public Decimal FeeToFireblock { get; set; }
        [DataMember(Order = 2)]public Decimal FeeOutsideFireblock { get; set; }
        [DataMember(Order = 3)]public Decimal FeeInFireblock { get; set; }
    }
}