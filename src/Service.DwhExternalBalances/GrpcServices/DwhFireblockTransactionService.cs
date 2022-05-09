using System.Threading.Tasks;
using Service.DwhExternalBalances.Grpc;
using Service.DwhExternalBalances.Grpc.Models;

namespace Service.DwhExternalBalances.GrpcServices
{
    public class DwhFireblockTransactionService :IDwhFireblockTransactionsService
    {

        private readonly FireblockTransactionsDwhRepositories _repositories;

        public DwhFireblockTransactionService(FireblockTransactionsDwhRepositories repositories)
        {
            _repositories = repositories;
        }


        public async Task<GetFireblokTransactionsResponse> GetTransactionWithinFireblockAsync(GetByDateRequest request)
        {
            var result = await _repositories.GetTransactionWithinFireblockAsync(request.DateFrom, request.DateTo);

            return new GetFireblokTransactionsResponse()
            {
                FireblockTransaction = result
            };
        }

        public async Task<GetFireblokTransactionsResponse> GetTransactionOutsideFireblockAsync(GetByDateRequest request)
        {
            var result = await _repositories.GetTransactionOutsideFireblockAsync(request.DateFrom, request.DateTo);

            return new GetFireblokTransactionsResponse()
            {
                FireblockTransaction = result
            };
        }

        public async Task<GetFireblokTransactionsResponse> GetTransactionToFireblockAsync(GetByDateRequest request)
        {
            var result = await _repositories.GetTransactionToFireblock(request.DateFrom, request.DateTo);

            return new GetFireblokTransactionsResponse()
            {
                FireblockTransaction = result
            };
        }

        public async Task<GetFireblockFeeResponse> GetFeeByDayAsync(GetByDateRequest request)
        {
            var result = await _repositories.GetFireblockFeeByDate(request.DateFrom, request.DateTo);

            return new GetFireblockFeeResponse()
            {
                FeeFireblock = result
            };
        }
    }
}