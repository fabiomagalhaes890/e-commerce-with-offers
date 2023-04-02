using Klir.TechChallenge.Domain.Base;
using Klir.TechChallenge.Domain.Base.Repositories;
using Klir.TechChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Klir.TechChallenge.Infrastructure.Data.Repositories
{
    public class Repository<TEntity> 
        : IRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly KlirDbContext _context;

        public Repository(KlirDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.AddAsync(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id == entity.Id);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }

            return entity;
        }

        public async Task<TEntity> UpdateByExpression(Expression<Func<TEntity, bool>> expression, TEntity entity)
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (result != null)
            {
                _context.Entry(result).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Set<TEntity>().FirstOrDefaultAsync(p => p.Id == id);

            if (result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }
    }
}
