using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
using Checkout.Kata.Services;
namespace Checkout.Kata.UnitTest
{
    [TestFixture]
    public class PricingTest
    {
        private PricingRule[] DefaultRules => new[]
        {
            new PricingRule("A", 50, 3, 130),
            new PricingRule("B", 30, 2, 45),
            new PricingRule("C", 20),
            new PricingRule("D", 15),
        };

        [Test]
        public void DefaultPricing_ShouldImplementIPrice()
        {
            var pricing = new Checkout.Kata.Services.DefaultPricing(new List<PricingRule>());
            Assert.That(pricing, Is.InstanceOf<IPricing>());
        }

        [Test]
        public void CalculateTotalFor_NoSpecials_ReturnsUnitTimesQty()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.Equals(20 * 3, svc.CalculateTotalFor('C', 3));
        }

        [Test]
        public void CalculateTotalFor_AppliesSpecialPrice()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.Equals(130, svc.CalculateTotalFor('A', 3));
            Assert.Equals(130 + 50, svc.CalculateTotalFor('A', 4));
        }

        [Test]
        public void CalculateTotalFor_UnknownSKU_Throws()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.Throws<ArgumentException>(() => svc.CalculateTotalFor('X', 1));
        }

    }
}
