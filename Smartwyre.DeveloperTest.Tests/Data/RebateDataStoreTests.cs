using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Data;

public class RebateDataStoreTests
{
    private readonly RebateDataStore _store = new();

    [Fact]
    public void GetRebate_KnownIdentifier_ReturnsNonNull()
    {
        var rebate = _store.GetRebate("cash");

        Assert.NotNull(rebate);
    }

    [Fact]
    public void GetRebate_DifferentCase_StillFound()
    {
        var rebate = _store.GetRebate("CASH");

        Assert.NotNull(rebate);
    }

    [Fact]
    public void GetRebate_UnknownIdentifier_ReturnsNull()
    {
        var rebate = _store.GetRebate("does-not-exist");

        Assert.Null(rebate);
    }

    [Fact]
    public void StoreCalculationResult_RecordsTheCalculation()
    {
        var rebate = new Rebate { Identifier = "test-rebate", Incentive = IncentiveType.FixedCashAmount, Amount = 50m, Percentage = 0m };
        var product = new Product { Identifier = "test-product", Price = 100m, Uom = "ton" };

        _store.StoreCalculationResult(rebate, product, 10m, 42m);

        var recorded = Assert.Single(_store.StoredCalculations);

        // Checked field by field so a mixed-up mapping (e.g. Percentage into
        // RebateAmount) fails here instead of passing silently.
        Assert.NotEqual(Guid.Empty, recorded.Id);
        Assert.Equal("test-rebate", recorded.RebateIdentifier);
        Assert.Equal("test-product", recorded.ProductIdentifier);
        Assert.Equal(IncentiveType.FixedCashAmount, recorded.IncentiveType);
        Assert.Equal(50m, recorded.RebateAmount);
        Assert.Equal(0m, recorded.RebatePercentage);
        Assert.Equal(100m, recorded.ProductPrice);
        Assert.Equal("ton", recorded.ProductUom);
        Assert.Equal(10m, recorded.Volume);
        Assert.Equal(42m, recorded.CalculatedAmount);
    }
}
