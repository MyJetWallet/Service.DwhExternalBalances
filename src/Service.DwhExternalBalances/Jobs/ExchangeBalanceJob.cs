using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.DataBase.Models;
using Service.Liquidity.InternalWallets.Grpc;
using Service.Liquidity.InternalWallets.Grpc.Models;

namespace Service.DwhExternalBalances.Jobs
{
    public class ExchangeBalanceJob : IStartable
    {
        private readonly MyTaskTimer _timer;
        private readonly ILogger<ExchangeBalanceJob> _logger;
        private readonly IExternalMarketsGrpc _externalMarketsGrpc;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;

        public ExchangeBalanceJob(
            ILogger<ExchangeBalanceJob> logger, 
            IExternalMarketsGrpc externalMarketsGrpc,
            IDwhDbContextFactory dwhDbContextFactory
            )
        {
            _logger = logger;
            _externalMarketsGrpc = externalMarketsGrpc;
            _dwhDbContextFactory = dwhDbContextFactory;

            _timer = new MyTaskTimer(nameof(ExchangeBalanceJob),
                TimeSpan.FromSeconds(60), _logger, DoTime);
        }
        
        private async Task DoTime()
        {
            await PersistExternalBalances();
        }

        private async Task PersistExternalBalances()
        {
            try
            {
                _logger.LogInformation($"PersistExternalBalances start at {DateTime.UtcNow}");
                
                var externalExchanges = await _externalMarketsGrpc.GetExternalMarketListAsync();
                var exchangesList = externalExchanges.Data.List ?? new List<string>();
                var iterationTime = DateTime.UtcNow;
                
                _logger.LogInformation("PersistExternalBalances find exchanges: {exchangesJson}.", 
                    JsonConvert.SerializeObject(exchangesList));
                
                var allBalances = new List<ExternalBalanceEntity>();
                foreach (var exchange in exchangesList)
                {
                    var balances = await _externalMarketsGrpc.GetBalancesAsync(new SourceDto()
                    {
                        Source = exchange
                    });
                    _logger.LogInformation("PersistExternalBalances find {balanceCount} for exchange: {exchangeJson}.", 
                        balances.Data.List.Count, exchange);
                    
                    allBalances.AddRange(balances.Data.List.Select(e => new ExternalBalanceEntity()
                    {
                        Exchange = exchange,
                        Asset = e.Asset,
                        Balance = (decimal) e.Balance,
                        BalanceDate = iterationTime.Date,
                        LastUpdateDate = iterationTime
                    }));
                }
                
                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.UpsertExternalBalances(allBalances);
                _logger.LogInformation("PersistExternalBalances saved {balanceCount} balances.", 
                    allBalances.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        public void Start()
        {
            _timer.Start();
        }
    }
}