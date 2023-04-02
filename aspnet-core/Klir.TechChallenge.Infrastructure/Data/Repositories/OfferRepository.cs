using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using Klir.TechChallenge.Infrastructure.Context;

namespace Klir.TechChallenge.Infrastructure.Data.Repositories
{
    public sealed class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(KlirDbContext context) : base(context) { }

        public Offer? GetActivatedProductById(int id)
        {
            var result = _context.Set<Offer>().FirstOrDefault(x => x.ProductId == id && x.Status);
            return result;
        }

        public IEnumerable<Offer> GetByProductId(int id)
        {
            var offers = _context.Set<Offer>().Where(x => x.ProductId == id);
            return offers;
        }
    }
}
