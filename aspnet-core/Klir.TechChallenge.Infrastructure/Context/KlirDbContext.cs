using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Klir.TechChallenge.Infrastructure.Context
{
    public class KlirDbContext : DbContext
    {
        public KlirDbContext(DbContextOptions<KlirDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
