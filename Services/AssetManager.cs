using AssetTracker.Models;

namespace AssetTracker.Services
{
    public class AssetManager
    {
        private readonly List<Asset> _assets = new();

        // Sample data (ChatGPT generated)
        public void SeedTestData(Dictionary<string, decimal> rates)
        {
            _assets.Add(AssetFactory.Create("Laptop", "Dell", "XPS 13", "Toronto", 1200m, DateTime.Parse("2023-05-01"), rates));
            _assets.Add(AssetFactory.Create("Phone", "Apple", "iPhone 14", "London", 999m, DateTime.Parse("2023-11-15"), rates));
            _assets.Add(AssetFactory.Create("Monitor", "Samsung", "Odyssey G7", "Berlin", 800m, DateTime.Parse("2023-10-10"), rates));
            _assets.Add(AssetFactory.Create("Laptop", "HP", "EliteBook 840", "London", 1100m, DateTime.Parse("2021-07-01"), rates));
            _assets.Add(AssetFactory.Create("Phone", "Samsung", "Galaxy S22", "Tokyo", 850m, DateTime.Parse("2022-09-01"), rates));
            _assets.Add(AssetFactory.Create("Monitor", "LG", "UltraFine 5K", "Toronto", 1300m, DateTime.Parse("2022-10-02"), rates));
            _assets.Add(AssetFactory.Create("Laptop", "Lenovo", "ThinkPad X1", "Boston", 1400m, DateTime.Parse("2023-06-15"), rates));
            _assets.Add(AssetFactory.Create("Phone", "Google", "Pixel 7", "Malmö", 700m, DateTime.Parse("2023-02-28"), rates));
            _assets.Add(AssetFactory.Create("Monitor", "ASUS", "ProArt Display", "Tokyo", 900m, DateTime.Parse("2021-08-20"), rates));
            _assets.Add(AssetFactory.Create("Laptop", "Apple", "MacBook Air M2", "London", 1200m, DateTime.Parse("2022-09-25"), rates));
            _assets.Add(AssetFactory.Create("Phone", "OnePlus", "11", "Berlin", 750m, DateTime.Parse("2022-08-01"), rates));
            _assets.Add(AssetFactory.Create("Monitor", "Dell", "U2723QE", "Malmö", 850m, DateTime.Parse("2024-03-01"), rates));
            _assets.Add(AssetFactory.Create("Laptop", "MSI", "Prestige 14", "Boston", 1000m, DateTime.Parse("2022-06-10"), rates));
            _assets.Add(AssetFactory.Create("Phone", "Sony", "Xperia 1 V", "Tokyo", 950m, DateTime.Parse("2022-12-15"), rates));
        }

        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
        }

        public IReadOnlyList<Asset> GetAllAssets()
        {
            // Sort by office then purchase date
            List<Asset> sortedAssets = _assets.OrderBy(a => a.Office)
                                               .ThenBy(a => a.PurchaseDate)
                                               .ToList();
            return sortedAssets.AsReadOnly();

        }
    }
}