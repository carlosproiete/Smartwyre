namespace Smartwyre.DeveloperTest.Tests.Fakes;

public class FakeTimeProvider(DateTimeOffset now) : TimeProvider
{
    public override DateTimeOffset GetUtcNow() => now;
}
