namespace Smartwyre.DeveloperTest.Types;

public enum SupportedIncentiveType
{
    FixedRateRebate = 1 << 0,
    AmountPerUom = 1 << 1,
    FixedCashAmount = 1 << 2,
    SeasonalRebate = 1 << 3,
    FortuneRebate = 1 << 4,
}
