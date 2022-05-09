using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.DwhExternalBalances.Grpc;

namespace Service.DwhExternalBalances.Client
{
    [UsedImplicitly]
    public class DwhExternalBalancesClientFactory: MyGrpcClientFactory
    {
        public DwhExternalBalancesClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IDwhExternalBalancesService GetDwhExternalBalancesService() => CreateGrpcService<IDwhExternalBalancesService>();

        public IDwhFireblockTransactionsService GetDwhFireblockTransactionsService() =>
            CreateGrpcService<IDwhFireblockTransactionsService>();
    }
}
