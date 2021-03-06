using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using MyJetWallet.Domain.ExternalMarketApi;
using MyJetWallet.Domain.ExternalMarketApi.Dto;
using MyJetWallet.Sdk.Service.Tools;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.Domain.Models;

namespace Service.DwhExternalBalances.Jobs
{
    public class ExchangeBalanceJob : IStartable
    {
        private readonly MyTaskTimer _timer;
        private readonly ILogger<ExchangeBalanceJob> _logger;
        private readonly IExternalMarket _externalMarket;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly IExternalExchangeManager _externalExchangeManager;

        public ExchangeBalanceJob(
            ILogger<ExchangeBalanceJob> logger,
            IExternalMarket externalMarket,
            IDwhDbContextFactory dwhDbContextFactory,
            IExternalExchangeManager externalExchangeManager
        )
        {
            _logger = logger;
            _externalMarket = externalMarket;
            _dwhDbContextFactory = dwhDbContextFactory;
            _externalExchangeManager = externalExchangeManager;

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
                ICollection<ExternalBalance> savedBalances;
                
                await using (var ctx = _dwhDbContextFactory.Create())
                {
                    savedBalances = await ctx.GetBalancesAsync();
                }

                var externalMarkets = await _externalExchangeManager.GetExternalExchangeCollectionAsync();

                foreach (var exchangeName in externalMarkets.ExchangeNames ?? new List<string>())
                {
                    try
                    {
                        var response = await _externalMarket.GetBalancesAsync(new GetBalancesRequest
                        {
                            ExchangeName = exchangeName
                        });

                        if (response?.Balances == null || !response.Balances.Any())
                        {
                            continue;
                        }
                        
                        var newBalanceAssets = response.Balances
                            .Select(b => b.Symbol)
                            .ToHashSet();
                        var zeroBalances = savedBalances
                            .Where(b => b.Type == exchangeName && !newBalanceAssets.Contains(b.Asset));

                        allBalances.AddRange(zeroBalances.Select(exchangeSavedBalance => new ExternalBalance
                        {
                            Asset = exchangeSavedBalance.Asset,
                            Type = exchangeName,
                            Volume = 0
                        }));
                        allBalances.AddRange(response.Balances.Select(e => new ExternalBalance
                            {
                                Asset = e.Symbol,
                                Type = exchangeName,
                                Volume = e.Balance
                            }
                        ));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Can't persist external balances. Failed to get balances from api");
                    }
                }

                if (allBalances.Any())
                {
                    try
                    {
                        await using var ctx = _dwhDbContextFactory.Create();
                        await ctx.UpsertExternalBalances(allBalances);
                        _logger.LogInformation("PersistExternalBalances saved {balanceCount} balances.",
                            allBalances.Count);
                    }
                    catch (SqlException e)
                    {
                        _logger.LogWarning(e, e.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to PersistExternalBalances. {ExMess}", ex.Message);
            }
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}