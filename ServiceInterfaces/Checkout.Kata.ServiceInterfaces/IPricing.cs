namespace Checkout.Kata.ServiceInterfaces
{
    public interface IPricing
    {
        int CalculateTotalFor(char sku, int quantity);
    }
}
