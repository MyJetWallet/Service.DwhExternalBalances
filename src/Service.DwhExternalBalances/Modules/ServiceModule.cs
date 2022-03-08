using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Sdk.NoSql;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Engines;
using Service.DwhExternalBalances.Jobs;
using Service.Fireblocks.Api.Client;
using Service.Fireblocks.Webhook.Client;
using Service.IndexPrices.Client;


namespace Service.DwhExternalBalances.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var noSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));

            builder.RegisterType<DwhDbContextFactory>().As<IDwhDbContextFactory>().SingleInstance();
            builder.RegisterType<IndexPriceJob>().As<IStartable>().AutoActivate().SingleInstance();
            builder.RegisterType<MarketPriceEngine>().AsSelf().SingleInstance();
            builder.RegisterType<ConvertPriceEngine>().AsSelf().SingleInstance();
            builder.RegisterType<ExchangeBalanceJob>().As<IStartable>().AutoActivate().SingleInstance();
            builder.RegisterConvertIndexPricesClient(noSqlClient);
            builder.RegisterCurrentPricesClient(noSqlClient);
            builder.RegisterExternalMarketClient(Program.Settings.ExternalApiGrpcUrl);
            builder.RegisterFireblocksWebhookCache(noSqlClient);
            builder.RegisterType<FireBlockJob>().As<IStartable>().AutoActivate().SingleInstance();
            builder.RegisterFireblocksApiClient(Program.Settings.FireblocksApiUrl);
            
            builder.RegisterType<FeeFireBlockJob>().As<IStartable>().AutoActivate().SingleInstance();
        }
    }
}