using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Fakes;

public class FakeRebateDataStore : IRebateDataStore
{
    public Rebate? RebateToReturn { get; set; }

    public List<(Rebate Rebate, Product Product, decimal Volume, decimal CalculatedAmount)> StoreCalls { get; } = new();

    public Rebate? GetRebate(string rebateIdentifier) => RebateToReturn;

    public void StoreCalculationResult(Rebate rebate, Product product, decimal volume, decimal calculatedAmount)
    {
        StoreCalls.Add((rebate, product, volume, calculatedAmount));
    }
}
