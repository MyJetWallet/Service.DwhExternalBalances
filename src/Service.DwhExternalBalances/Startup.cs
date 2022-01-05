using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Autofac;
using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.GrpcMetrics;
using MyJetWallet.Sdk.GrpcSchema;
using MyJetWallet.Sdk.Service;
using Prometheus;
using ProtoBuf.Grpc.Server;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Grpc;
using Service.DwhExternalBalances.GrpcServices;
using Service.DwhExternalBalances.Modules;
using Service.DwhExternalBalances.Services;
using SimpleTrading.BaseMetrics;
using SimpleTrading.ServiceStatusReporterConnector;

namespace Service.DwhExternalBalances
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.BindCodeFirstGrpc();

            services.AddHostedService<ApplicationLifetimeManager>();

            services.AddMyTelemetry("SP-", Program.Settings.ZipkinUrl);
            
            DwhContext.LoggerFactory = Program.LogFactory;
            AddDatabase(services, Program.Settings.DwhConnectionString);
            DwhContext.LoggerFactory = null;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseMetricServer();

            app.BindServicesTree(Assembly.GetExecutingAssembly());

            app.BindIsAlive();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcSchema<DwhExternalBalancesesService, IDwhExternalBalancesService>();

                endpoints.MapGrpcSchemaRegistry();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<SettingsModule>();
            builder.RegisterModule<ServiceModule>();
        }
        
        private static void AddDatabase(IServiceCollection services, string connectionString)
        {
            if (!connectionString.Contains("ApplicationName"))
            {
                
                var appName = ApplicationEnvironment.HostName;
                if (appName == null)
                {
                    appName = Assembly.GetEntryAssembly()?.GetName().Name;
                }

                connectionString = connectionString.Last() != ';' 
                    ? $"{connectionString};Application Name={appName}" 
                    : $"{connectionString}Application Name={appName}";
            }

            services.AddSingleton(x =>
            {
                var optionsBuilder = new DbContextOptionsBuilder<DwhContext>();
                optionsBuilder.UseSqlServer(connectionString,
                    builder =>
                        builder.MigrationsHistoryTable(
                            $"__EFMigrationsHistory_DwhExternalBalances",
                            DwhContext.Schema));

                return optionsBuilder;
            });

            var contextOptions = services.BuildServiceProvider().GetRequiredService<DbContextOptionsBuilder<DwhContext>>();

            using var activity = MyTelemetry.StartActivity("database migration");
            {
                Console.WriteLine("======= begin database migration =======");
                var sw = new Stopwatch();
                sw.Start();
                
                using var context = new DwhContext(contextOptions.Options);

                context.Database.Migrate();

                sw.Stop();
                Console.WriteLine($"======= end database migration ({sw.Elapsed.ToString()}) =======");
            }
        }
    }
}
