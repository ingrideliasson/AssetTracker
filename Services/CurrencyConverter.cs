namespace AssetTracker.Services
{
    public class CurrencyConverter
    {
        public static decimal Convert(decimal amount, string targetCurrency, Dictionary<string, decimal> rates)
        {
            if (!rates.ContainsKey(targetCurrency))
            {
                throw new ArgumentException("Currency not found.");
            }
            return amount * rates[targetCurrency];
        }
    }
}
