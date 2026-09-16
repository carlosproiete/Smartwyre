using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedRateRebateCalculator : IRebateAmountCalculator
{
    public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume)
    {
        var result = new RebateAmountCalculationResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
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

        result.Success = true;
        result.CalculatedAmount = product.Price * rebate.Percentage * volume;
        return result;
    }
}
