using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FixedCashAmountCalculator : IRebateAmountCalculator
{
    public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume)
    {
        var result = new RebateAmountCalculationResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        {
            result.Reason = "Product does not support this incentive type";
            return result;
        }

        if (rebate.Amount == 0)
        {
            result.Reason = "Rebate amount is zero";
            return result;
        }

        result.Success = true;
        result.CalculatedAmount = rebate.Amount;
        return result;
    }
}
