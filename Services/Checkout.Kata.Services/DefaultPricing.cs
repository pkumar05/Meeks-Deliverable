using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
using Microsoft.Extensions.Logging;

namespace Checkout.Kata.Services
{
    public class DefaultPricing : IPricing
    {
        private readonly IReadOnlyDictionary<string, PricingRule> _pricingRule;
        private readonly ILogger<Checkout> _logger;

        public DefaultPricing(IEnumerable<PricingRule> pricingRule, ILogger<Checkout> logger = null)
        {
            if (pricingRule == null) throw new ArgumentNullException(nameof(pricingRule));
            _pricingRule = pricingRule.ToDictionary(r => r.SKU);
            _logger = logger;
        }
        public int CalculateTotalFor(char sku, int quantity)
        {
            var total = 0;
            _logger?.LogInformation($"Calculated Total Price: {total}");
            return total;
        }
    }
}
