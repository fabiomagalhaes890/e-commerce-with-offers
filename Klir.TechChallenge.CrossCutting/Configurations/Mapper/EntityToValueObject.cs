using AutoMapper;
using Klir.TechChallenge.Application.Offers;
using Klir.TechChallenge.Domain.AggregateModel.Offers;

namespace Klir.TechChallenge.CrossCutting.Configurations.Mapper
{
    public class EntityToValueObject : Profile
    {
        public EntityToValueObject()
        {
            CreateMap<Offer, OfferValueObject>();
        }
    }
}
