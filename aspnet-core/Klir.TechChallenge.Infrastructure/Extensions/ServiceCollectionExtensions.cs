using Klir.TechChallenge.Application.Services.Checkouts;
using Klir.TechChallenge.CrossCutting.Configurations.Mapper;
using Klir.TechChallenge.Domain.AggregateModel.Offers;
using Klir.TechChallenge.Domain.Base.Repositories;
using Klir.TechChallenge.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Offer>, Repository<Offer>>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            services.AddScoped<ICheckoutService, CheckoutService>();

            var assembly = AppDomain.CurrentDomain.Load("Klir.TechChallenge.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(
                typeof(IRequestToEntity),
                typeof(EntityToValueObject));

            return services;
        }
    }
}
