using Klir.TechChallenge.Domain.AggregateModel.Offers;
using System.Linq.Expressions;

namespace Klir.TechChallenge.Domain.Base.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> UpdateByExpression(Expression<Func<TEntity, bool>> expression, TEntity entity);
    }
}
