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

        public CheckoutValueObject Checkout(ShoppingCartValueObject shoppingCart)
        {
            var response = new CheckoutValueObject();

            _ = shoppingCart.Products.Select(x =>
            {
                var offer = _repository.GetActivatedProductById(x.Product.Id);

                if (offer != null && x.Count >= offer.Quantity)
                {
                    response.Total += (x.Count * x.Product.Price);
                    response.Discount += offer.CalculateDiscount(x.Count, x.Product.Price);
                    response.TotalWithDiscount = (response.Total - response.Discount);
                }
                else
                {
                    response.TotalWithDiscount += (x.Count * x.Product.Price);
                    response.Total += (x.Count * x.Product.Price);
                }

                return response;
            }).ToList();

            return response;
        }
    }
}
