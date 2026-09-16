using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests.Fakes;

public class FakeProductDataStore : IProductDataStore
{
    public Product ProductToReturn { get; set; }

    public Product GetProduct(string productIdentifier) => ProductToReturn;
}
