using AutoMapper;
using Klir.TechChallenge.Domain.Base.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.Offers.Queries
{
    public class OfferQueryHandler : IRequestHandler<OfferQueryByProductId, IEnumerable<OfferValueObject>>
    {
        private readonly IOfferRepository _repository;
        private readonly IMapper _mapper;

        public OfferQueryHandler(
            IOfferRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IEnumerable<OfferValueObject>> Handle(OfferQueryByProductId request, CancellationToken cancellationToken)
        {
            var offers = _repository.GetByProductId(request.ProductId);
            var result = _mapper.Map<IEnumerable<OfferValueObject>>(offers);
            return Task.FromResult(result);
        }
    }
}
