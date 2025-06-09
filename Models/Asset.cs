
using System.Drawing;
using System.Dynamic;

namespace AssetTracker.Models
{
    public class Asset
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Office { get; set; }
        public Price PriceUSD { get; set; }
        public Price PriceLocal { get; set; }
        public DateTime PurchaseDate { get; set; }

        public Asset(string type, string brand, string model, string office, Price priceUSD, Price priceLocal, DateTime purchaseDate)
        {
            Type = type;
            Brand = brand;
            Model = model;
            Office = office;
            PriceUSD = priceUSD;
            PriceLocal = priceLocal;
            PurchaseDate = purchaseDate;
        }


        public override string ToString()
        {
            return $"{Type}\t{Brand}\t{Model}\t{PriceUSD.Amount} {PriceUSD.Currency.Symbol}\t{PurchaseDate.ToShortDateString()}";
        }
    }
}

