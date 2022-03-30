using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using Serilog;
using Service.DwhExternalBalances.DataBase;
using Service.Fireblocks.Api.Grpc;
using Service.Fireblocks.Api.Grpc.Models.GasStation;

namespace Service.DwhExternalBalances.Jobs
{
    public class GasStationBalanceJob: IStartable
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly IGasStationService _gasStationService;
        private readonly ILogger<GasStationBalanceJob> _logger;
        private readonly MyTaskTimer _timer;

        public GasStationBalanceJob(IDwhDbContextFactory dwhDbContextFactory, 
            IGasStationService gasStationService,
            ILogger<GasStationBalanceJob> logger)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
            _gasStationService = gasStationService;
            _logger = logger;
            _timer = new MyTaskTimer(nameof(GasStationBalanceJob), TimeSpan.FromSeconds(60), _logger, DoTime);
        }
        
        private async Task DoTime()
        {
            await GetGasStationBalances();
        }

        private async Task GetGasStationBalances()
        {
            try
            {
                var balances = await _gasStationService.GetGasStationAsync(new GetGasStationRequest { });
                var balancesList = balances.Balances?.ToList() ?? new List<GasStationBalance>();

                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.UpdateGasStationBalances(balancesList);
                _logger.LogInformation("GasStation Balances updated {balances}", balancesList.Count);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Cannot update GasStationBalances");
            }
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}