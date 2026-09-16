using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateAmountCalculatorFactoryTests
{
    private readonly RebateAmountCalculatorFactory _factory = new();

    [Theory]
    [InlineData(IncentiveType.FixedCashAmount, typeof(FixedCashAmountCalculator))]
    [InlineData(IncentiveType.FixedRateRebate, typeof(FixedRateRebateCalculator))]
    [InlineData(IncentiveType.AmountPerUom, typeof(AmountPerUomCalculator))]
    public void GetCalculator_KnownIncentiveType_ReturnsMatchingCalculator(IncentiveType incentiveType, Type expectedType)
    {
        var calculator = _factory.GetCalculator(incentiveType);

        Assert.IsType(expectedType, calculator);
    }

    [Fact]
    public void GetCalculator_UnknownIncentiveType_ThrowsArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _factory.GetCalculator((IncentiveType)999));
    }
}
