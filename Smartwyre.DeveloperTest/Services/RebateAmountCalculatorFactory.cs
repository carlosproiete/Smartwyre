using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateAmountCalculatorFactory : IRebateAmountCalculatorFactory
{
    public IRebateAmountCalculator GetCalculator(IncentiveType incentiveType) => incentiveType switch
    {
        IncentiveType.FixedCashAmount => new FixedCashAmountCalculator(),
        IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
        IncentiveType.AmountPerUom => new AmountPerUomCalculator(),
        _ => throw new ArgumentOutOfRangeException(nameof(incentiveType), incentiveType, "No calculator registered for this incentive type."),
    };
}
