using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class SeasonalRebateCalculator(TimeProvider timeProvider) : IRebateAmountCalculator
{
    public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume)
    {
        var result = new RebateAmountCalculationResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.SeasonalRebate))
        {
            result.Reason = "Product does not support this incentive type";
            return result;
        }

        if (rebate.Percentage == 0)
        {
            result.Reason = "Rebate percentage is zero";
            return result;
        }

        if (product.Price == 0)
        {
            result.Reason = "Product price is zero";
            return result;
        }

        if (volume <= 0)
        {
            result.Reason = "Volume must be greater than zero";
            return result;
        }

        var seasonMultiplier = GetSeasonMultiplier(timeProvider.GetUtcNow().Month);

        result.Success = true;
        result.CalculatedAmount = product.Price * rebate.Percentage * volume * seasonMultiplier;
        return result;
    }

    // Fall is harvest season for soy, rice and corn: reward volume with a bonus.
    // Winter is off-season: reduced rate. Hardcoded for this exercise; a real
    // implementation would likely make this configurable per crop and region.
    private static decimal GetSeasonMultiplier(int month) => month switch
    {
        3 or 4 or 5 => 1.0m,
        6 or 7 or 8 => 1.0m,
        9 or 10 or 11 => 1.5m,
        _ => 0.5m,
    };
}
