using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class AmountPerUomCalculatorTests
{
    private readonly AmountPerUomCalculator _calculator = new();

    [Fact]
    public void Calculate_ValidInput_ReturnsSuccessWithAmount()
    {
        var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 2m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(20m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductSupportsMultipleIncentiveTypes_StillSucceeds()
    {
        var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 2m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom | SupportedIncentiveType.FixedCashAmount };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(20m, result.CalculatedAmount);
    }

    [Fact]
    public void Calculate_ProductDoesNotSupportIncentive_ReturnsFailure()
    {
        var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = 2m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedRateRebate };

        var result = _calculator.Calculate(rebate, product, 10m);

        Assert.False(result.Success);
        Assert.Equal("Product does not support this incentive type", result.Reason);
    }

    [Theory]
    [InlineData(0, 10, "Rebate amount is zero")]
    [InlineData(2, 0, "Volume must be greater than zero")]
    [InlineData(2, -5, "Volume must be greater than zero")]
    public void Calculate_InvalidInput_ReturnsFailureWithReason(decimal amount, decimal volume, string expectedReason)
    {
        var rebate = new Rebate { Incentive = IncentiveType.AmountPerUom, Amount = amount };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.AmountPerUom };

        var result = _calculator.Calculate(rebate, product, volume);

        Assert.False(result.Success);
        Assert.Equal(expectedReason, result.Reason);
    }
}
