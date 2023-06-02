using Klir.TechChallenge.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infrastructure.Extensions
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("default");
            services.AddDbContext<KlirDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, KlirDbContext>();

            return services;
        }
    }
}
