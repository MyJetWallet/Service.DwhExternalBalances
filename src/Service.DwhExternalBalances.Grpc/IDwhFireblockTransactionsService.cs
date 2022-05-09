using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Threading.Tasks;
using Service.DwhExternalBalances.Grpc.Models;

namespace Service.DwhExternalBalances.Grpc;

[ServiceContract]
public interface IDwhFireblockTransactionsService
{
    [OperationContract]
    Task<GetFireblokTransactionsResponse> GetTransactionWithinFireblockAsync(GetByDateRequest request);
    
    [OperationContract]
    Task<GetFireblokTransactionsResponse> GetTransactionOutsideFireblockAsync(GetByDateRequest request);
    
    [OperationContract]
    Task<GetFireblokTransactionsResponse> GetTransactionToFireblockAsync(GetByDateRequest request);

    [OperationContract]
    Task<GetFireblockFeeResponse> GetFeeByDayAsync(GetByDateRequest request);
}