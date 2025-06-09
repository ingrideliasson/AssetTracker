using System.Reflection;
using AssetTracker.Models;
using AssetTracker.Services;
using AssetTracker.Helpers;
using System.Globalization;
using System.Drawing;
using AssetTracker.Infrastructure;

namespace AssetTracker.UI
{
    public class ConsoleUI
    {
        private readonly AssetManager _assetManager;

        public ConsoleUI(AssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public async Task RunAsync()
        {
            CurrencyRates liveRates = await LiveCurrencyFetcher.FetchRatesAsync(); // Fetch rates
            var rates = liveRates.ConversionRates; // Dictionary mapping from currency codes to rates

            if (rates != null)
            {
                _assetManager.SeedTestData(rates);
            }
            else
            {
                Console.WriteLine("Error: Currency rates could not be fetched. Exiting...");
                return;
            }

            while (true)
            {
                ShowAssets(); // Start by showing the assets (sample data at start of the program)
                Console.WriteLine("Enter 'a' to add an asset or 'q' to quit the program.");
                string input = Console.ReadLine() ?? "".ToLower().Trim();

                switch (input)
                {
                    case "a":
                        UserAddAsset(rates);
                        break;

                    case "q":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid input.");
                        break;
                }
            }
        }

        public string PromptOffice() // Helper method to keep asking for an office location until it is valid
        {
            while (true)
            {
                Console.WriteLine("Enter an office location (London/Tokyo/Berlin/Malmö/Boston): ");
                string input = Console.ReadLine() ?? "";
                if (OfficeToCurrency.IsValidOffice(input))
                {
                    return input;
                }
                Console.WriteLine("Invalid office location. Please try again.");
            }
        }

        public decimal PromptPrice() // Helper method to keep asking for a valid price
        {
            while (true)
            {
                Console.WriteLine("Enter a price in USD (use comma for decimals): ");
                string input = Console.ReadLine() ?? "";
                if (decimal.TryParse(input, out var price) && price > 0)
                {
                    return price;
                }
                Console.WriteLine("Invalid input, please enter a positive number.");
            }
        }

        public DateTime PromptDate() // Helper method to keep asking for a valid date
        {
            while (true)
            {
                Console.WriteLine("Enter a purchase date (DD/MM/YYYY): ");
                string input = Console.ReadLine() ?? "";
                if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                else
                {
                    Console.Write("Invalid date format. Please use DD/MM/YYYY (e.g. 30/06/2025): ");
                }
            }
        }

        public void UserAddAsset(Dictionary<string, decimal> rates)
        {
            Console.WriteLine("Enter an asset type (e.g. 'laptop'): ");
            string inputType = Console.ReadLine() ?? "";
            Console.WriteLine("Enter a brand (e.g. 'Asus'): ");
            string inputBrand = Console.ReadLine() ?? "";
            Console.WriteLine("Enter a model (e.g. X2000): ");
            string inputModel = Console.ReadLine() ?? "";
            decimal inputPriceUSD = PromptPrice();
            string inputOffice = PromptOffice();
            DateTime inputDate = PromptDate();

            Currency targetCurrency = OfficeToCurrency.GetCurrency(inputOffice); // Get target currency from office
            decimal convertedAmount = CurrencyConverter.Convert(inputPriceUSD, targetCurrency.Code, rates); // Convert USD to target currency

            Price priceLocal = new Price(convertedAmount, targetCurrency);
            Price priceUSD = new Price(inputPriceUSD, Currency.USD);

            Asset asset = AssetFactory.Create(inputType, inputBrand, inputModel, inputOffice, inputPriceUSD, inputDate, rates); // Use the helper method to add an asset in a simpler way
            _assetManager.AddAsset(asset);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAsset successfully added!");
            Console.ResetColor();
        }


        public void ShowAssets()
        {

            Console.Clear();
            Console.WriteLine("Here are all the current assets:");
            var assets = _assetManager.GetAllAssets();

            // print header
            Console.WriteLine("{0,-10} | {1,-10} | {2,-15} | {3,-10} | {4,10} | {5,12} | {6,-14}",
            "Type", "Brand", "Model", "Office", "USD Price", "Local Price", "Purchase Date");
            Console.WriteLine(new string('-', 100));

            foreach (var asset in assets)
            {
                (string assetColor, bool isExpired) = GetExpirationDate.CheckDate(asset.PurchaseDate); // Check expiration date of asset and which color to print in

                // Add emoji if expired
                string purchaseDateDisplay = asset.PurchaseDate.ToString("yyyy-MM-dd");
                if (isExpired)
                {
                    purchaseDateDisplay += " ❌"; // Expired symbol
                }

                // Build the output string
                string output = string.Format(
                "{0,-10} | {1,-10} | {2,-15} | {3,-10} | {4,10:C} | {5,12:C} | {6,-14}",
                asset.Type,
                asset.Brand,
                asset.Model,
                asset.Office,
                asset.PriceUSD,
                asset.PriceLocal,
                purchaseDateDisplay
            );

                // Set console color
                if (isExpired || assetColor == "red")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (assetColor == "yellow")
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }

                Console.WriteLine(output);
                Console.WriteLine();
                Console.ResetColor();
            }
        }
    }
}