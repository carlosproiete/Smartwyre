namespace Smartwyre.DeveloperTest.Types;

public record RebateCalculation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public DateTimeOffset CalculatedAt { get; init; } = DateTimeOffset.UtcNow;
    public required string RebateIdentifier { get; init; }
    public required string ProductIdentifier { get; init; }
    public required IncentiveType IncentiveType { get; init; }
    public required decimal RebateAmount { get; init; }
    public required decimal RebatePercentage { get; init; }
    public required decimal ProductPrice { get; init; }
    public required string ProductUom { get; init; }
    public required decimal Volume { get; init; }
    public required decimal CalculatedAmount { get; init; }
}
