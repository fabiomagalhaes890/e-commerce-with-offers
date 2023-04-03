namespace Klir.TechChallenge.Application.Services.Checkouts
{
    public interface ICheckoutService
    {
        CheckoutValueObject Checkout(ShoppingCartValueObject shoppingCart);
    }
}
