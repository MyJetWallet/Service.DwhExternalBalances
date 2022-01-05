using System.ServiceModel;
using System.Threading.Tasks;
using Service.DwhExternalBalances.Grpc.Models;

namespace Service.DwhExternalBalances.Grpc
{
    [ServiceContract]
    public interface IHelloService
    {
        [OperationContract]
        Task<HelloMessage> SayHelloAsync(HelloRequest request);
    }
}