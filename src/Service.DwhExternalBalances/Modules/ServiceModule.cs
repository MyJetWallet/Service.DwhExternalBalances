﻿using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Service.DwhExternalBalances.DataBase;

namespace Service.DwhExternalBalances.Modules
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DwhDbContextFactory>().As<IDwhDbContextFactory>().SingleInstance();
        }
    }
}