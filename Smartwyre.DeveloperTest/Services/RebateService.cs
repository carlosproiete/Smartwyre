using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService(
    IRebateDataStore rebateDataStore,
    IProductDataStore productDataStore,
    IRebateAmountCalculatorFactory calculatorFactory) : IRebateService
{
    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        if (rebate is null)
        {
            return new CalculateRebateResult { Success = false, Reason = "Rebate not found" };
        }

        var product = productDataStore.GetProduct(request.ProductIdentifier);
        if (product is null)
        {
            return new CalculateRebateResult { Success = false, Reason = "Product not found" };
        }

        var calculator = calculatorFactory.GetCalculator(rebate.Incentive);
        var outcome = calculator.Calculate(rebate, product, request.Volume);

        if (outcome.Success)
        {
            rebateDataStore.StoreCalculationResult(rebate, product, request.Volume, outcome.CalculatedAmount);
        }

        return new CalculateRebateResult
        {
            Success = outcome.Success,
            Reason = outcome.Reason,
            CalculatedAmount = outcome.CalculatedAmount,
        };
    }
}
