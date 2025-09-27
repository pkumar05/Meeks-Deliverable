using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace Checkout.Kata.Services
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, PricingRule> _pricingRules;
        private readonly Dictionary<string, int> _counts = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _scannedItems = new();
        private readonly ILogger<Checkout> _logger;

        public Checkout(IEnumerable<PricingRule> pricingRules, ILogger<Checkout> logger = null)
        {
            _pricingRules = pricingRules.ToDictionary(p => p.SKU, p => p);
            _logger = logger;
        }

        public int GetTotalPrice()
        {
            var total = 0;
            _logger?.LogInformation($"Calculated Total Price: {total}");
            return total;
        }

        public void Scan(string item)
        {
            _logger?.LogInformation($"Scanned item: {item}");
        }
    }
}
