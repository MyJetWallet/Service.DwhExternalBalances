using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.DataBase.Models;
using Service.DwhExternalBalances.Jobs;
using Service.IndexPrices.Client;

namespace Service.DwhExternalBalances.Engines
{
    public class AssetsUsdPricesEngine
    {
        private readonly IDwhDbContextFactory _dwhDbContextFactory;
        private readonly IIndexPricesClient _indexPricesClient;
        private readonly ILogger<AssetsUsdPricesEngine> _logger;

        public AssetsUsdPricesEngine(IDwhDbContextFactory dwhDbContextFactory, 
            IIndexPricesClient indexPricesClient, 
            ILogger<AssetsUsdPricesEngine> logger)
        {
            _dwhDbContextFactory = dwhDbContextFactory;
            _indexPricesClient = indexPricesClient;
            _logger = logger;
        }
        
        public async Task GetIndexPricesForAssets()
        {
            try
            {
                var indexPrices = _indexPricesClient.GetIndexPricesAsync();
                await using var ctx = _dwhDbContextFactory.Create();
                List<IndexPricesEntity> entities = new List<IndexPricesEntity>();
                foreach (var item in indexPrices)
                {
                    entities.Add(new IndexPricesEntity()
                    {
                        Asset = item.Asset,
                        UpdateDate = item.UpdateDate,
                        UsdPrice = item.UsdPrice
                    });
                }
                await ctx.UpdateIndexPrices(entities);
                _logger.LogInformation("AssetsUsdPrices update {price}", indexPrices.Count);
            }
            catch (Exception e)
            {
                _logger.LogError("AssetsUsdPrices: {err}",e.Message);
            }
        }
        
        public async Task UpdateIndexPricesForAssetsSnapshot()
        {
            try
            {
                var snapshot = _indexPricesClient.GetIndexPricesAsync();
                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.UpdateIndexPriceShapshot(snapshot);
                _logger.LogInformation("AssetsUsdPricesSnapshot update {price}",snapshot.Count);
            }
            catch (Exception e)
            {
                _logger.LogError("AssetsUsdPricesSnapshot: {err}",e.Message);
            }
        }
    }
}