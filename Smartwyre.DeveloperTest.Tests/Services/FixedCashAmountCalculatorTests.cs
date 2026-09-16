using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class FixedCashAmountCalculatorTests
{
    private readonly FixedCashAmountCalculator _calculator = new();

    [Fact]
    public void Calculate_ValidInput_ReturnsSuccessWithAmount()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 50m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(50m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductSupportsMultipleIncentiveTypes_StillSucceeds()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 50m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount | SupportedIncentiveType.FixedRateRebate };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(50m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductDoesNotSupportIncentive_ReturnsFailure()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 50m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.False(result.Success);
        Assert.Equal("Product does not support this incentive type", result.Reason);
    }

    [Fact]
    public void Calculate_RebateAmountIsZero_ReturnsFailure()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount, Amount = 0m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.False(result.Success);
        Assert.Equal("Rebate amount is zero", result.Reason);
    }
}
