using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class FortuneRebateCalculator(Random random) : IRebateAmountCalculator
{
    private const int WinChancePercent = 20;

    public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume)
    {
        var result = new RebateAmountCalculationResult();

        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FortuneRebate))
        {
            result.Reason = "Product does not support this incentive type";
            return result;
        }

        if (rebate.Amount == 0)
        {
            result.Reason = "Rebate amount is zero";
            return result;
        }

        var won = random.Next(100) < WinChancePercent;

        // Not winning is a valid outcome, not a validation failure.
        result.Success = true;
        result.CalculatedAmount = won ? rebate.Amount : 0m;
        return result;
    }
}
