namespace Klir.TechChallenge.Application.Services.Checkouts
{
    public interface ICheckoutService
    {
        ShoppingCartValueObject Checkout(ShoppingCartValueObject shoppingCart);
    }
}
