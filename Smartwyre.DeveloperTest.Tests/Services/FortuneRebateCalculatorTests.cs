using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class FortuneRebateCalculatorTests
{
    [Fact]
    public void Calculate_ValidInput_SucceedsRegardlessOfOutcome()
    {
        var calculator = new FortuneRebateCalculator(Random.Shared);
        var rebate = new Rebate { Incentive = IncentiveType.FortuneRebate, Amount = 100m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FortuneRebate };

        var result = calculator.Calculate(rebate, product, 1m);

        Assert.True(result.Success);
        Assert.True(result.CalculatedAmount == 0m || result.CalculatedAmount == rebate.Amount);
    }

    [Fact]
    public void Calculate_ProductDoesNotSupportIncentive_ReturnsFailure()
    {
        var calculator = new FortuneRebateCalculator(new Random());
        var rebate = new Rebate { Incentive = IncentiveType.FortuneRebate, Amount = 100m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        var result = calculator.Calculate(rebate, product, 1m);

        Assert.False(result.Success);
        Assert.Equal("Product does not support this incentive type", result.Reason);
    }

    [Fact]
    public void Calculate_RebateAmountIsZero_ReturnsFailure()
    {
        var calculator = new FortuneRebateCalculator(new Random());
        var rebate = new Rebate { Incentive = IncentiveType.FortuneRebate, Amount = 0m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FortuneRebate };

        var result = calculator.Calculate(rebate, product, 1m);

        Assert.False(result.Success);
        Assert.Equal("Rebate amount is zero", result.Reason);
    }
}
