using AutoMapper;
using Klir.TechChallenge.Application.Offers.Command;
using Klir.TechChallenge.Domain.AggregateModel.Offers;

namespace Klir.TechChallenge.CrossCutting.Configurations.Mapper
{
    public class IRequestToEntity : Profile
    {
        public IRequestToEntity()
        {
            CreateMap<OfferCreateCommand, Offer>();
            CreateMap<OfferUpdateCommand, Offer>();
        }
    }
}
