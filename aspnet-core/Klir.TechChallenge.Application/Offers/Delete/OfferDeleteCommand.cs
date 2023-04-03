using MediatR;

namespace Klir.TechChallenge.Application.Offers.Delete
{
    public class OfferDeleteCommand : IRequest
    {
        public int Id { get; set; }
    }
}
