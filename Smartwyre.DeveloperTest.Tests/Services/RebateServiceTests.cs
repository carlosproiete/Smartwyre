using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Tests.Fakes;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceTests
{
    [Fact]
    public void Calculate_RebateNotFound_ReturnsFailureWithoutStoringAnything()
    {
        var rebateStore = new FakeRebateDataStore { RebateToReturn = null };
        var productStore = new FakeProductDataStore();
        var factory = new FakeRebateAmountCalculatorFactory();
        var service = new RebateService(rebateStore, productStore, factory);

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "unknown", ProductIdentifier = "soy", Volume = 1m });

        Assert.False(result.Success);
        Assert.Equal("Rebate not found", result.Reason);
        Assert.Empty(rebateStore.StoreCalls);
    }

    [Fact]
    public void Calculate_ProductNotFound_ReturnsFailureWithoutStoringAnything()
    {
        var rebateStore = new FakeRebateDataStore { RebateToReturn = new Rebate { Incentive = IncentiveType.FixedCashAmount } };
        var productStore = new FakeProductDataStore { ProductToReturn = null };
        var factory = new FakeRebateAmountCalculatorFactory();
        var service = new RebateService(rebateStore, productStore, factory);

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "cash", ProductIdentifier = "unknown", Volume = 1m });

        Assert.False(result.Success);
        Assert.Equal("Product not found", result.Reason);
        Assert.Empty(rebateStore.StoreCalls);
    }

    [Fact]
    public void Calculate_CalculatorSucceeds_StoresResultAndReturnsSuccess()
    {
        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product();
        var rebateStore = new FakeRebateDataStore { RebateToReturn = rebate };
        var productStore = new FakeProductDataStore { ProductToReturn = product };
        var factory = new FakeRebateAmountCalculatorFactory { ResultToReturn = new RebateAmountCalculationResult { Success = true, CalculatedAmount = 42m } };
        var service = new RebateService(rebateStore, productStore, factory);

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "cash", ProductIdentifier = "soy", Volume = 10m });

        Assert.True(result.Success);
        Assert.Equal(42m, result.CalculatedAmount);
        var stored = Assert.Single(rebateStore.StoreCalls);
        Assert.Equal(rebate, stored.Rebate);
        Assert.Equal(product, stored.Product);
        Assert.Equal(10m, stored.Volume);
        Assert.Equal(42m, stored.CalculatedAmount);
    }

    [Fact]
    public void Calculate_CalculatorFails_DoesNotStoreAnythingAndReturnsReason()
    {
        var rebateStore = new FakeRebateDataStore { RebateToReturn = new Rebate { Incentive = IncentiveType.FixedCashAmount } };
        var productStore = new FakeProductDataStore { ProductToReturn = new Product() };
        var factory = new FakeRebateAmountCalculatorFactory { ResultToReturn = new RebateAmountCalculationResult { Success = false, Reason = "Rebate amount is zero" } };
        var service = new RebateService(rebateStore, productStore, factory);

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "cash", ProductIdentifier = "soy", Volume = 10m });

        Assert.False(result.Success);
        Assert.Equal("Rebate amount is zero", result.Reason);
        Assert.Empty(rebateStore.StoreCalls);
    }

    [Fact]
    public void Calculate_NewIncentiveTypeRegisteredInFactory_WorksWithoutChangingRebateService()
    {
        // Simulates adding a new incentive type: RebateService never inspects
        // IncentiveType itself, it only delegates to whatever the factory returns.
        var rebate = new Rebate { Incentive = (IncentiveType)999 };
        var rebateStore = new FakeRebateDataStore { RebateToReturn = rebate };
        var productStore = new FakeProductDataStore { ProductToReturn = new Product() };
        var factory = new FakeRebateAmountCalculatorFactory { ResultToReturn = new RebateAmountCalculationResult { Success = true, CalculatedAmount = 99m } };
        var service = new RebateService(rebateStore, productStore, factory);

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "future", ProductIdentifier = "soy", Volume = 1m });

        Assert.True(result.Success);
        Assert.Equal(99m, result.CalculatedAmount);
    }
}
