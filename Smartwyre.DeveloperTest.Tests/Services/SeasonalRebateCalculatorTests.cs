using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Tests.Fakes;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class SeasonalRebateCalculatorTests
{
    [Fact]
    public void Calculate_HarvestSeason_AppliesBonusMultiplier()
    {
        var calculator = new SeasonalRebateCalculator(new FakeTimeProvider(new DateTimeOffset(2026, 10, 15, 0, 0, 0, TimeSpan.Zero)));
        var rebate = new Rebate { Incentive = IncentiveType.SeasonalRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.SeasonalRebate, Price = 400m };

        var result = calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(300m, result.CalculatedAmount); // 400 * 0.05 * 10 * 1.5
    }

    [Fact]
    public void Calculate_OffSeason_AppliesReducedMultiplier()
    {
        var calculator = new SeasonalRebateCalculator(new FakeTimeProvider(new DateTimeOffset(2026, 1, 15, 0, 0, 0, TimeSpan.Zero)));
        var rebate = new Rebate { Incentive = IncentiveType.SeasonalRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.SeasonalRebate, Price = 400m };

        var result = calculator.Calculate(rebate, product, 10m);

        Assert.True(result.Success);
        Assert.Equal(100m, result.CalculatedAmount); // 400 * 0.05 * 10 * 0.5
    }

    [Fact]
    public void Calculate_ProductDoesNotSupportIncentive_ReturnsFailure()
    {
        var calculator = new SeasonalRebateCalculator(new FakeTimeProvider(DateTimeOffset.UtcNow));
        var rebate = new Rebate { Incentive = IncentiveType.SeasonalRebate, Percentage = 0.05m };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Price = 400m };

        var result = calculator.Calculate(rebate, product, 10m);

        Assert.False(result.Success);
        Assert.Equal("Product does not support this incentive type", result.Reason);
    }

    [Theory]
    [InlineData(0, 400, 10, "Rebate percentage is zero")]
    [InlineData(0.05, 0, 10, "Product price is zero")]
    [InlineData(0.05, 400, 0, "Volume must be greater than zero")]
    public void Calculate_InvalidInput_ReturnsFailureWithReason(decimal percentage, decimal price, decimal volume, string expectedReason)
    {
        var calculator = new SeasonalRebateCalculator(new FakeTimeProvider(DateTimeOffset.UtcNow));
        var rebate = new Rebate { Incentive = IncentiveType.SeasonalRebate, Percentage = percentage };
        var product = new Product { SupportedIncentives = SupportedIncentiveType.SeasonalRebate, Price = price };

        var result = calculator.Calculate(rebate, product, volume);

        Assert.False(result.Success);
        Assert.Equal(expectedReason, result.Reason);
    }
}
