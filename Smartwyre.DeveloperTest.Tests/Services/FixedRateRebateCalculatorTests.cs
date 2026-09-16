using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class FixedRateRebateCalculatorTests
{
    private readonly FixedRateRebateCalculator _calculator = new();

    [Fact]
    public void Calculate_ValidInput_ReturnsSuccessWithAmount()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = 400m };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(200m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductSupportsMultipleIncentiveTypes_StillSucceeds()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate | SupportedIncentiveType.AmountPerUom, Price = 400m };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(200m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductDoesNotSupportIncentive_ReturnsFailure()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 400m };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.False(result.Success);
        Assert.Equal("Product does not support this incentive type", result.Reason);
    }

    [Theory]
    [InlineData(0, 400, 10, "Rebate percentage is zero")]
    [InlineData(0.05, 0, 10, "Product price is zero")]
    [InlineData(0.05, 400, 0, "Volume must be greater than zero")]
    [InlineData(0.05, 400, -5, "Volume must be greater than zero")]
    public void Calculate_InvalidInput_ReturnsFailureWithReason(decimal percentage, decimal price, decimal volume, string expectedReason)
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedRateRebate, Percentage = percentage };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate, Price = price };

        var result = _calculator.Calculate(rebate, product, volume);

        Assert.False(result.Success);
        Assert.Equal(expectedReason, result.Reason);
    }
}
