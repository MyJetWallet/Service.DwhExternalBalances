using System.ServiceModel;
using System.Threading.Tasks;
using Service.DwhExternalBalances.Domain.Models;
using Service.DwhExternalBalances.Grpc.Models;

namespace Service.DwhExternalBalances.Grpc
{
    [ServiceContract]
    public interface IDwhExternalBalancesService
    {
        [OperationContract]
        Task<GetAllBalancesResponse> GetAllBalancesAsync();

        [OperationContract]
        Task UpsertBankBalanceAsync(ExternalBalance externalBalance);
        
        [OperationContract]
        Task UpsertFireblocksBalanceAsync(ExternalBalance externalBalance);

        [OperationContract]
        Task UpsertDeFiBalanceAsync(ExternalBalance externalBalance);

        [OperationContract]
        Task UpsertBinanceFtxBalanceAsync(ExternalBalance externalBalance);


    }
}