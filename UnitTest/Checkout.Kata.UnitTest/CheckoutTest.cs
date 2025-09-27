using Checkout.Kata.Rules;
using Checkout.Kata.ServiceInterfaces;
namespace Checkout.Kata.UnitTest
{
    [TestFixture]
    public class CheckoutTest
    {
        private static IEnumerable<PricingRule> DefaultPricing() => new[]
        {
            new PricingRule("A", 50, 3, 130),
            new PricingRule("B", 30, 2, 45),
            new PricingRule("C", 20),
            new PricingRule("D", 15),
        };

        [Test]
        public void Checkout_ShouldImplementICheckout()
        {
            var checkout = new Checkout.Kata.Services.Checkout(new List<PricingRule>());
            Assert.That(checkout, Is.InstanceOf<ICheckout>());
        }

        [Test]
        public void Unknown_Sku_Throws()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            Assert.Throws<InvalidOperationException>(() => checkout.Scan("Z"));
        }

    }
}
