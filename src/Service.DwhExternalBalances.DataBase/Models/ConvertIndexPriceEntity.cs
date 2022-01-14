using Service.IndexPrices.Domain.Models;

namespace Service.DwhExternalBalances.DataBase.Models
{
    public class ConvertIndexPriceEntity : ConvertIndexPrice
    {
        public ConvertIndexPriceEntity()
        {
        }

        public ConvertIndexPriceEntity(ConvertIndexPrice baseEntity)
        {
            this.BaseAsset = baseEntity.BaseAsset;
            this.QuotedAsset = baseEntity.QuotedAsset;
            this.Price = baseEntity.Price;
            this.UpdateDate = baseEntity.UpdateDate;
            this.Error = baseEntity.Error;
        }
    }
}