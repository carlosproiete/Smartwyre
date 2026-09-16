using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

// Named after the original "rebateAmount" variable, what this actually
// calculates, while staying distinct from RebateCalculation.
public interface IRebateAmountCalculator
{
    RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume);
}
