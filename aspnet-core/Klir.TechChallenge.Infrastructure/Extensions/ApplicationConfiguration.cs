using Klir.TechChallenge.Application.Services.Checkouts;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using Klir.TechChallenge.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infrastructure.Extensions
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Offer>, Repository<Offer>>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            services.AddScoped<ICheckoutService, CheckoutService>();

            return services;
        }
    }
}
