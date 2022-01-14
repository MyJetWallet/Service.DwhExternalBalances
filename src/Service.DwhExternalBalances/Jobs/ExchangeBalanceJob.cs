using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Domain.ExternalMarketApi.Dto;
using MyJetWallet.Sdk.Service.Tools;
using MyNoSqlServer.Abstractions;
using Newtonsoft.Json;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.DataBase.Models;
using Service.DwhExternalBalances.Domain.Models;


namespace Service.DwhExternalBalances.Jobs
{
    public class ExchangeBalanceJob : IStartable
    {
        private readonly MyTaskTimer _timer;
        private readonly ILogger<ExchangeBalanceJob> _logger;
        private readonly IExternalMarket _externalMarket;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        
        private readonly List<string> Exchange = new List<string>(){"Binance", "FTX"};


        public ExchangeBalanceJob(
            ILogger<ExchangeBalanceJob> logger,
            IExternalMarket externalMarket,
            IDwhDbContextFactory dwhDbContextFactory)
        {
            _logger = logger;
            _externalMarket = externalMarket;
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
                var allBalances = new List<ExternalBalance>();
                foreach (var ex in Exchange)
                {
                    var balance = await _externalMarket.GetBalancesAsync(new GetBalancesRequest()
                    {
                        ExchangeName = ex
                    });
                    
                    allBalances.AddRange(balance.Balances.Select(e=> new ExternalBalance()
                        {
                            Asset = e.Symbol,
                            Name = ex,
                            Type = ex,
                            Volume = e.Balance
                        }
                    ));
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