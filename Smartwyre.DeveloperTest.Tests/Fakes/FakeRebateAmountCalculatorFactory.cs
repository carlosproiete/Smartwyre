using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Fakes;

public class FakeRebateAmountCalculatorFactory : IRebateAmountCalculatorFactory
{
    public RebateAmountCalculationResult ResultToReturn { get; set; }

    public IRebateAmountCalculator GetCalculator(IncentiveType incentiveType) => new FakeCalculator(ResultToReturn);

    private class FakeCalculator(RebateAmountCalculationResult result) : IRebateAmountCalculator
    {
        public RebateAmountCalculationResult Calculate(Rebate rebate, Product product, decimal volume) => result;
    }
}
