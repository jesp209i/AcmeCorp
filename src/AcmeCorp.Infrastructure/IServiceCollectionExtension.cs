using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AcmeCorp.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddInfrastructureServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            return services;
        }
    }
}
