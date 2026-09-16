using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class AmountPerUomCalculator : IRebateAmountCalculator
{
    public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume)
    {
        var result = new RebateAmountCalculationResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        {
            result.Reason = "Product does not support this incentive type";
            return result;
        }

        if (rebate.Amount == 0)
        {
            result.Reason = "Rebate amount is zero";
            return result;
        }

        if (volume <= 0)
        {
            result.Reason = "Volume must be greater than zero";
            return result;
        }

        result.Success = true;
        result.CalculatedAmount = rebate.Amount * volume;
        return result;
    }
}
