namespace Checkout.Kata.ServiceInterfaces
{
    public interface IPricing
    {
        int CalculateTotalFor(string sku, int quantity);
    }
}
