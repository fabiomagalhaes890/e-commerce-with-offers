using MediatR;

namespace Klir.TechChallenge.Application.Offers.Queries
{
    public class OfferQueryByProductId : IRequest<IEnumerable<OfferValueObject>>
    {
        public int ProductId { get; set; }
    }
}
