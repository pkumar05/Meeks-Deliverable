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

        [Test]
        public void Scan_One_Item_A_50()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void Scan_ABCD_115()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            checkout.Scan("A");
            checkout.Scan("B");
            checkout.Scan("C");
            checkout.Scan("D");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(115));
        }

        [Test]
        public void Scanning_B_A_B_Applies_B_Special()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            checkout.Scan("B");
            checkout.Scan("A");
            checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(95)); // two B's => 45, one A => 50 -> total 95
        }

        [Test]
        public void Three_A_Special_Price()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
        }

        [Test]
        public void Mixed_A_and_B_Specials_And_Remainder()
        {
            var checkout = new Checkout.Kata.Services.Checkout(DefaultPricing());
            // AAA BBB -> A special (130), B: two for 45 + one 30 => 75, total 205
            checkout.Scan("A"); checkout.Scan("A"); checkout.Scan("A");
            checkout.Scan("B"); checkout.Scan("B"); checkout.Scan("B");
            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(205));
        }

    }
}
