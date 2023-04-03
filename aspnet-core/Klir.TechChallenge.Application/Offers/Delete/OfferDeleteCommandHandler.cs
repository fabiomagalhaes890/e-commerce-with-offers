using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.Offers.Delete
{
    public class OfferDeleteCommandHandler : IRequestHandler<OfferDeleteCommand>
    {
        private readonly IRepository<Offer> _repository;

        public OfferDeleteCommandHandler(IRepository<Offer> repository)
        {
            _repository = repository;
        }

        public async Task Handle(OfferDeleteCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
        }
    }
}
