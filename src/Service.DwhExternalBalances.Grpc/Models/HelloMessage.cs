using System.Runtime.Serialization;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Grpc.Models
{
    [DataContract]
    public class HelloMessage : IHelloMessage
    {
        [DataMember(Order = 1)]
        public string Message { get; set; }
    }
}