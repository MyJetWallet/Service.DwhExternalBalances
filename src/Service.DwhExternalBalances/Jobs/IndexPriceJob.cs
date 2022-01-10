using System;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using Serilog;
using Service.DwhExternalBalances.Engines;
using ILogger = Serilog.ILogger;

namespace Service.DwhExternalBalances.Jobs
{
    public class IndexPriceJob : IStartable
    {
        private readonly MyTaskTimer _timer;
        private readonly ILogger<IndexPriceJob> _logger;
        private readonly MarketPriceEngine _marketPriceEngine;
        private readonly ConvertPriceEngine _convertPriceEngine;

        public IndexPriceJob(
            MarketPriceEngine marketPriceEngine,
            ConvertPriceEngine convertPriceEngine,
            ILogger<IndexPriceJob> logger)
        {
            _logger = logger;
            _marketPriceEngine = marketPriceEngine;
            _convertPriceEngine = convertPriceEngine;
            _timer = new MyTaskTimer(nameof(IndexPriceJob), TimeSpan.FromSeconds(60), _logger, DoTime);
        }

        private async Task DoTime()
        {
            await _marketPriceEngine.HandleMarketPrice();
            await _convertPriceEngine.HandleConvertPrice();
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}