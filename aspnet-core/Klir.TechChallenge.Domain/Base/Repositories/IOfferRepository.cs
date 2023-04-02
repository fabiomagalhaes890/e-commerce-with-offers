using Klir.TechChallenge.Domain.AggregateModel.Offers;

namespace Klir.TechChallenge.Domain.Base.Repositories
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Offer? GetActivatedProductById(int id);
        IEnumerable<Offer> GetByProductId(int id);
    }
}
