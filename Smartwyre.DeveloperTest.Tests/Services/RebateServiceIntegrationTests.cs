using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Services;

public class RebateServiceIntegrationTests
{
    [Fact]
    public void Calculate_RealDataStoresAndFactory_ProducesExpectedRebate()
    {
        var service = new RebateService(new RebateDataStore(), new ProductDataStore(), new RebateAmountCalculatorFactory());

        var result = service.Calculate(new CalculateRebateRequest { RebateIdentifier = "rate", ProductIdentifier = "soy", Volume = 10m });

        Assert.True(result.Success);
        Assert.Equal(200m, result.CalculatedAmount);
    }
}
