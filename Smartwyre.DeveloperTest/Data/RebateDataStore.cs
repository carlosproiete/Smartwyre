using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore : IRebateDataStore
{
    private static readonly Dictionary<string, Rebate> SampleRebates = new(StringComparer.OrdinalIgnoreCase)
    {
        ["cash"] = new Rebate { Identifier = "cash", Incentive = IncentiveType.FixedCashAmount, Amount = 50m },
        ["rate"] = new Rebate { Identifier = "rate", Incentive = IncentiveType.FixedRateRebate, Percentage = 0.05m },
        ["uom"] = new Rebate { Identifier = "uom", Incentive = IncentiveType.AmountPerUom, Amount = 2m },
        ["season"] = new Rebate { Identifier = "season", Incentive = IncentiveType.SeasonalRebate, Percentage = 0.05m },
        ["fortune"] = new Rebate { Identifier = "fortune", Incentive = IncentiveType.FortuneRebate, Amount = 100m },
    };

    public List<RebateCalculation> StoredCalculations { get; } = new();

    public Rebate? GetRebate(string rebateIdentifier)
    {
        // Sample data for this exercise. A real implementation would query a database.
        return SampleRebates.GetValueOrDefault(rebateIdentifier);
    }

    public void StoreCalculationResult(Rebate rebate, Product product, decimal volume, decimal calculatedAmount)
    {
        // Id and CalculatedAt self-generate on construction.
        StoredCalculations.Add(new RebateCalculation
        {
            RebateIdentifier = rebate.Identifier,
            ProductIdentifier = product.Identifier,
            IncentiveType = rebate.Incentive,
            RebateAmount = rebate.Amount,
            RebatePercentage = rebate.Percentage,
            ProductPrice = product.Price,
            ProductUom = product.Uom,
            Volume = volume,
            CalculatedAmount = calculatedAmount,
        });
    }
}
