using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateAmountCalculatorFactory
{
    IRebateAmountCalculator GetCalculator(IncentiveType incentiveType);
}
