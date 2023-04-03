using MediatR;

namespace Klir.TechChallenge.Application.Offers.Command
{
    public class OfferCreateCommand : CommandBase, IRequest<OfferValueObject>
    {        
    }
}
