using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateService = new RebateService(new RebateDataStore(), new ProductDataStore(), new RebateAmountCalculatorFactory());

        while (true)
        {
            Console.Write("Rebate identifier (or 'exit' to quit): ");
            var rebateIdentifier = Console.ReadLine();

            if (rebateIdentifier is null || string.Equals(rebateIdentifier, "exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.Write("Product identifier: ");
            var productIdentifier = Console.ReadLine();

            if (productIdentifier is null)
            {
                Console.WriteLine("Product identifier is required.");
                Console.WriteLine();
                continue;
            }

            Console.Write("Volume: ");
            var volumeInput = Console.ReadLine();

            if (!decimal.TryParse(volumeInput, out var volume))
            {
                Console.WriteLine("Invalid volume, please enter a number.");
                Console.WriteLine();
                continue;
            }

            try
            {
                var request = new CalculateRebateRequest
                {
                    RebateIdentifier = rebateIdentifier,
                    ProductIdentifier = productIdentifier,
                    Volume = volume,
                };

                var result = rebateService.Calculate(request);

                Console.WriteLine(result.Success
                    ? $"Success. Calculated amount: {result.CalculatedAmount:F2}"
                    : $"Failed: {result.Reason}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"{DateTimeOffset.UtcNow:O} rebate={rebateIdentifier} product={productIdentifier} volume={volumeInput}: {ex}");
                Console.WriteLine("Something went wrong calculating the rebate.");
            }

            Console.WriteLine();
        }
    }
}
