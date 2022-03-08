using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;
using Serilog;
using Service.AssetsDictionary.Client;
using Service.AssetsDictionary.Grpc;
using Service.DwhExternalBalances.DataBase;

namespace Service.DwhExternalBalances.Jobs
{
    public class DictionariesJob
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly ILogger<DictionariesJob> _logger;
        private readonly IMarketReferencesDictionaryService _marketReferencesDictionaryService;
        private readonly IAssetsDictionaryService _assetsDictionaryService;
        private readonly ISpotInstrumentsDictionaryService _spotInstrumentsDictionaryService;
        private readonly MyTaskTimer _timer;

        public DictionariesJob(IDwhDbContextFactory dwhDbContextFactory, 
            ILogger<DictionariesJob> logger, 
            IMarketReferencesDictionaryService marketReferencesDictionaryService, 
            IAssetsDictionaryService assetsDictionaryService, 
            ISpotInstrumentsDictionaryService spotInstrumentsDictionaryService)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
            _logger = logger;
            _marketReferencesDictionaryService = marketReferencesDictionaryService;
            _assetsDictionaryService = assetsDictionaryService;
            _spotInstrumentsDictionaryService = spotInstrumentsDictionaryService;

            _timer = new MyTaskTimer(nameof(DictionariesJob), TimeSpan.FromSeconds(300), _logger, DoTime);
        }

        private async Task DoTime()
        {
            await GetDictionaries();
        }

        private async Task GetDictionaries()
        {
            try
            {
                var assets = await _assetsDictionaryService.GetAllAssetsAsync();
                var marketReference = await _marketReferencesDictionaryService.GetAllMarketReferencesAsync();
                var spotInstrument = await _spotInstrumentsDictionaryService.GetAllSpotInstrumentsAsync();


                await using var ctx = _dwhDbContextFactory.Create();
                await using var tr = ctx.Database.BeginTransaction();
                
                await ctx.Database.ExecuteSqlRawAsync("TRUNCATE TABLE data.AssetsDictionary");
                _logger.LogInformation("AssetsDictionaty table was truncated");
                await ctx.UpsertAssetDictionary(assets.Assets);
                _logger.LogInformation("AssetsDictionary added {assets}", assets.Assets.ToList().Count);
                
                await ctx.Database.ExecuteSqlRawAsync("TRUNCATE TABLE data.SpotInstrument");
                _logger.LogInformation("SpotInstrument table was truncated");
                await ctx.UpsertSpotInstruments(spotInstrument.SpotInstruments);
                _logger.LogInformation("SpotInstrument added {spotInstrument}",spotInstrument.SpotInstruments.ToList().Count);
                
                await ctx.Database.ExecuteSqlRawAsync("TRUNCATE TABLE data.MarketReference");
                _logger.LogInformation("MarketReference table was truncated");
                await ctx.UpsertMarketReference(marketReference.References);
                _logger.LogInformation("MarketReference added {marketReference}",marketReference.References.ToList().Count);
                await tr.CommitAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        public void Start()
        {
            _timer.Start();
        }
    }
}