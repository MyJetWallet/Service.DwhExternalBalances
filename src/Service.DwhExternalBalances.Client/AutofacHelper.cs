using Autofac;
using Service.DwhExternalBalances.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.DwhExternalBalances.Client
{
    public static class AutofacHelper
    {
        public static void RegisterDwhExternalBalancesClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new DwhExternalBalancesClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetDwhExternalBalancesService()).As<IDwhExternalBalancesService>().SingleInstance();

            builder.RegisterInstance(factory.GetDwhFireblockTransactionsService())
                .As<IDwhFireblockTransactionsService>().SingleInstance();
        }
    }
}
