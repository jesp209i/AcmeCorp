using Microsoft.Extensions.DependencyInjection;
using AcmeCorp.Infrastructure.Interfaces;

namespace AcmeCorp.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddSingleton<IProductFetcher, ProductFetcher>();
            return services;
        }
    }
}
