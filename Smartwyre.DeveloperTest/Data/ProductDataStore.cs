using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore : IProductDataStore
{
    private static readonly Dictionary<string, Product> SampleProducts = new(StringComparer.OrdinalIgnoreCase)
    {
        ["soy"] = new Product { Id = 1, Identifier = "soy", Price = 400m, Uom = "ton", SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.SeasonalRebate | SupportedIncentiveType.FortuneRebate },
        ["rice"] = new Product { Id = 2, Identifier = "rice", Price = 350m, Uom = "ton", SupportedIncentives = SupportedIncentiveType.FixedCashAmount },
        ["corn"] = new Product { Id = 3, Identifier = "corn", Price = 200m, Uom = "ton", SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom },
    };

    public Product GetProduct(string productIdentifier)
    {
        // Sample data for this exercise. A real implementation would query a database.
        return SampleProducts.GetValueOrDefault(productIdentifier);
    }
}
