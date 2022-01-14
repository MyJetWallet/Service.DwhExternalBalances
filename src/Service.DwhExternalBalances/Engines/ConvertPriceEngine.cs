using System;
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
            //var pricesEntities = prices.Select(e => new ConvertIndexPriceEntity(e)).ToList();

            var priceGroupping = prices.Select(e => new ConvertIndexPriceEntity(e)).ToList();

            foreach (var item in priceGroupping)
            {
                await using var ctx = _dwhDbContextFactory.Create();
                await ctx.SinglUpsertConvertPrice(item);
            }

            /*var pricesEntities =
                priceGroupping.GroupBy(i => $"{i.BaseAsset} - {i.QuotedAsset}")
                    .Select(i => i.OrderByDescending(i => i.UpdateDate).First())
                    .ToList().Chunk(100);

            try
            {
                await using var ctx = _dwhDbContextFactory.Create();
                foreach (var items in pricesEntities)
                {
                    await ctx.UpsertConvertPrice(items);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }*/
        }
    }
}