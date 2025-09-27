namespace Checkout.Kata.Rules
{
    public class PricingRule
    {
        public string SKU { get; set; }
        public int UnitPrice { get; set; }
        public int? SpecialQuantity { get; set; }
        public int? SpecialPrice { get; set; }

        public PricingRule(string sku, int unitPrice, int? specialQuantity = null, int? specialPrice = null)
        {
            if (string.IsNullOrWhiteSpace(sku)) throw new ArgumentException("SKU is empty!");
            if (unitPrice < 0) throw new ArgumentOutOfRangeException(nameof(unitPrice));

            SKU = sku;
            UnitPrice = unitPrice;
            SpecialQuantity = specialQuantity;
            SpecialPrice = specialPrice;

        }
    }
}
