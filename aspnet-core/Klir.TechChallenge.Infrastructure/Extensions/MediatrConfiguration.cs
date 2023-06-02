using Klir.TechChallenge.CrossCutting.Configurations.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace Klir.TechChallenge.Infrastructure.Extensions
{
    public static class MediatrConfiguration
    {
        public static IServiceCollection RegisterMediatr(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Klir.TechChallenge.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(
                typeof(IRequestToEntity),
                typeof(EntityToValueObject));

            return services;
        }
    }
}
