namespace Smartwyre.DeveloperTest.Services;

// Separate from CalculateRebateResult: this is the internal strategy's own
// result shape, not the public Calculate operation's response DTO.
public class RebateAmountCalculationResult
{
    public bool Success { get; set; }
    public string Reason { get; set; } = string.Empty;
    public decimal CalculatedAmount { get; set; }
}
