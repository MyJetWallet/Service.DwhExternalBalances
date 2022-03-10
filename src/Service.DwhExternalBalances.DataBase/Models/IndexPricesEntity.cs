namespace Service.DwhExternalBalances.DataBase.Models;

public class IndexPricesEntity
{
    public string Asset { get; set; }
    public Decimal UsdPrice { get; set; }
    public DateTime UpdateDate { get; set; }
}