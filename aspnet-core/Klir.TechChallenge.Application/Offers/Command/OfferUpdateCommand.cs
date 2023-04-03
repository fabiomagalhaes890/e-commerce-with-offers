using MediatR;

namespace Klir.TechChallenge.Application.Offers.Command
{
    public class OfferUpdateCommand : CommandBase, IRequest<OfferValueObject>
    {        
    }
}
