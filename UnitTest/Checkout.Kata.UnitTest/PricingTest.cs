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
        public void For_NoSpecials_ReturnsUnitTimesQty()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.That(20*3, Is.EqualTo(svc.CalculateTotalFor("C", 3)));
        }

        [Test]
        public void For_AppliesSpecialPrice()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.That(130, Is.EqualTo(svc.CalculateTotalFor("A", 3)));
            Assert.That(130 + 50, Is.EqualTo(svc.CalculateTotalFor("A", 4)));
        }

        [Test]
        public void For_UnknownSKU_Throws()
        {
            var svc = new DefaultPricing(DefaultRules);
            Assert.Throws<KeyNotFoundException>(() => svc.CalculateTotalFor("X", 1));
            
        }

    }
}
