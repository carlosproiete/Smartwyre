using Smartwyre.DeveloperTest.Data;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Data;

public class ProductDataStoreTests
{
    private readonly ProductDataStore _store = new();

    [Fact]
    public void GetProduct_KnownIdentifier_ReturnsNonNull()
    {
        var product = _store.GetProduct("soy");

        Assert.NotNull(product);
    }

    [Fact]
    public void GetProduct_DifferentCase_StillFound()
    {
        var product = _store.GetProduct("SOY");

        Assert.NotNull(product);
    }

    [Fact]
    public void GetProduct_UnknownIdentifier_ReturnsNull()
    {
        var product = _store.GetProduct("does-not-exist");

        Assert.Null(product);
    }
}
