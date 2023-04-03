using Klir.TechChallenge.Domain.Base.Repositories;

namespace Klir.TechChallenge.Application.Services.Checkouts
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IOfferRepository _repository;

        public CheckoutService(IOfferRepository repository)
        {
            _repository = repository;
        }

        public ShoppingCartValueObject Checkout(ShoppingCartValueObject shoppingCart)
        {
            shoppingCart.Checkout = new CheckoutValueObject();
            _ = shoppingCart.Products.Select(x =>
            {
                var offer = _repository.GetActivatedProductById(x.Product.Id);

                if (offer != null && x.Count >= offer.Quantity)
                {
                    shoppingCart.Checkout.Total += (x.Count * x.Product.Price);
                    shoppingCart.Checkout.Discount += offer.CalculateDiscount(x.Count, x.Product.Price);
                    shoppingCart.Checkout.TotalWithDiscount = (shoppingCart.Checkout.Total - shoppingCart.Checkout.Discount);
                    x.PromoApplied = offer.Name;
                    x.TotalPrice = (x.Count * x.Product.Price) - offer.CalculateDiscount(x.Count, x.Product.Price);
                }
                else
                {
                    x.PromoApplied = string.Empty;
                    shoppingCart.Checkout.TotalWithDiscount += (x.Count * x.Product.Price);
                    shoppingCart.Checkout.Total += (x.Count * x.Product.Price);
                }

                return x;
            }).ToList();

            return shoppingCart;
        }
    }
}
