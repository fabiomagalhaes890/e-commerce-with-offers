using AutoMapper;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using MediatR;

namespace Klir.TechChallenge.Application.Offers.Command
{
    public class OfferCommandHandler 
        : IRequestHandler<OfferCreateCommand, OfferValueObject>
        , IRequestHandler<OfferUpdateCommand, OfferValueObject>
    {
        private readonly IOfferRepository _repository;
        private readonly IMapper _mapper;

        public OfferCommandHandler(IOfferRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OfferValueObject> Handle(OfferCreateCommand request, CancellationToken cancellationToken)
        {
            var result = await Persist(request);
            return result;
        }

        public async Task<OfferValueObject> Handle(OfferUpdateCommand request, CancellationToken cancellationToken)
        {
            var result = await Persist(request);
            return result;
        }

        private async Task<OfferValueObject> Persist(CommandBase request)
        {
            try
            {
                var offer = _mapper.Map<Offer>(request);

                var activeOffer = _repository.GetActivatedProductById(request.ProductId);

                if (activeOffer != null)
                {
                    activeOffer.Status = false;
                    await _repository.UpdateByExpression(o => o.ProductId == request.ProductId && o.Status, activeOffer);
                }

                if (request.Id != 0)
                    offer = await _repository.UpdateAsync(offer);
                else
                    offer = await _repository.AddAsync(offer);

                var offerValueObject = _mapper.Map<OfferValueObject>(offer);
                
                return offerValueObject;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error has ocurred while persisting data: {ex.Message}");
            }
        }
    }
}
