using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AcmeCorp.Application
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServiceDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
