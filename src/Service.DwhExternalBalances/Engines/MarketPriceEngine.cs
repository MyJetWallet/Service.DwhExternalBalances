using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.DataBase.Models;
using Service.IndexPrices.Client;

namespace Service.DwhExternalBalances.Engines
{
    public class MarketPriceEngine
    {
        private readonly ICurrentPricesClient _currentPricesClient;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;

        public MarketPriceEngine(ICurrentPricesClient currentPricesClient, IDwhDbContextFactory dwhDbContextFactory)
        {
            _currentPricesClient = currentPricesClient;
            _dwhDbContextFactory = dwhDbContextFactory;
        }

        public async Task HandleMarketPrice()
        {
            var price = _currentPricesClient.GetAllPrices();
            var pricesEntity = price.Select(e => new MarketPriceEntity(e)).ToList();

            await using var ctx = _dwhDbContextFactory.Create();
            await ctx.UpsertMarketPrice(pricesEntity);
        }
    }
}