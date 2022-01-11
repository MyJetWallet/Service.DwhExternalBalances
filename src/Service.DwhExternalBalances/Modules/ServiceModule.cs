using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Engines;
using Service.DwhExternalBalances.Jobs;

namespace Service.DwhExternalBalances.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DwhDbContextFactory>().As<IDwhDbContextFactory>().SingleInstance();
            builder.RegisterType<IndexPriceJob>().As<IStartable>().AutoActivate().SingleInstance();
            builder.RegisterType<ExchangeBalanceJob>().As<IStartable>().AutoActivate().SingleInstance();
            builder.RegisterType<MarketPriceEngine>().AsSelf().SingleInstance();
            builder.RegisterType<ConvertPriceEngine>().AsSelf().SingleInstance();
        }
    }
}