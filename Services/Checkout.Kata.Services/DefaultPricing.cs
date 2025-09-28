using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
namespace Checkout.Kata.Services
{
    public class DefaultPricing : IPricing
    {
        private readonly IReadOnlyDictionary<string, PricingRule> _rules;

        public DefaultPricing(IEnumerable<PricingRule> rules)
        {
            if (rules == null) throw new ArgumentNullException(nameof(rules));
            _rules = rules.ToDictionary(r => r.SKU);
        }

        public int CalculateTotalFor(string key, int quantity)
        {
            var total = 0;
            var sku = key;
            var count = quantity;
            var rule = _rules[sku];

            if (rule.SpecialQuantity.HasValue)
            {
                int q = rule.SpecialQuantity.Value;
                int specials = count / q;
                int remainder = count % q;
                total += specials * rule.SpecialPrice.Value + remainder * rule.UnitPrice;
            }
            else
            {
                total += count * rule.UnitPrice;
            }
            return total;
        }


    }
}
