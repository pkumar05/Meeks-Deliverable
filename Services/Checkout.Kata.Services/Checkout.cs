using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
using Checkout.Kata.Services.Helper;
using Microsoft.Extensions.Logging;

namespace Checkout.Kata.Services
{
    public class Checkout : ICheckout
    {
        private readonly Dictionary<string, PricingRule> _pricingRules;
        private readonly Dictionary<string, int> _counts = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _scannedItems = new();
        private readonly IPricing _pricing;
        private readonly ILogger<Checkout> _logger;

        public Checkout(IEnumerable<PricingRule> rules)
            : this(rules, new DefaultPricing(rules), new SimpleNullLogger<Checkout>())
        {
        }

        public Checkout(IEnumerable<PricingRule> pricingRules,IPricing pricing, ILogger<Checkout> logger = null)
        {
            _pricingRules = pricingRules.ToDictionary(p => p.SKU, p => p) ?? throw new ArgumentNullException(nameof(pricingRules));
            _pricing =pricing ?? throw new ArgumentNullException(nameof(pricing));
            _logger = logger;
        }

        public int GetTotalPrice()
        {
            int total = 0;
            foreach (var x in _counts)
            {
                total += _pricing.CalculateTotalFor(x.Key, x.Value);
            }
         
            _logger?.LogInformation($"Calculated Total Price: {total}");
            return total;
        }

        public void Scan(string item)
        {
            if (string.IsNullOrWhiteSpace(item) || item.Length != 1)
                throw new ArgumentException("Scanning item must be a single character SKU", nameof(item));

            var sku = item;
            if (!_pricingRules.ContainsKey(sku))
                throw new InvalidOperationException($"Unknown SKU '{sku}'.");

            if (!_counts.ContainsKey(sku)) _counts[sku] = 0;
            _counts[sku]++;
            _logger?.LogInformation($"Scanned item: {item}");
        }
    }
}
