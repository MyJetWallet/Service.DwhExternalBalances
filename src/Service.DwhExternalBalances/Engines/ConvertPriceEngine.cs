using System.Linq;
using System.Threading.Tasks;
using Service.DwhExternalBalances.DataBase;
using Service.DwhExternalBalances.DataBase.Models;
using Service.IndexPrices.Client;

namespace Service.DwhExternalBalances.Engines
{
    public class ConvertPriceEngine
    {
        private readonly IConvertIndexPricesClient _convertIndexPricesClient;
        private readonly IDwhDbContextFactory _dwhDbContextFactory;

        public ConvertPriceEngine(IConvertIndexPricesClient convertIndexPricesClient, IDwhDbContextFactory dwhDbContextFactory)
        {
            _convertIndexPricesClient = convertIndexPricesClient;
            _dwhDbContextFactory = dwhDbContextFactory;
        }
        
        public async Task HandleConvertPrice()
        {
            var prices = _convertIndexPricesClient.GetConvertIndexPricesAsync();
            var pricesEntities = prices.Select(e => new ConvertIndexPriceEntity(e)).ToList();

            await using var ctx = _dwhDbContextFactory.Create();
            await ctx.UpsertConvertPrice(pricesEntities);
        }
    }
}