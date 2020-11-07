using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace AcmeCorp.Persistance
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddPersistanceServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICompetitionRepository, CompetitionRepository>();
            return services;
        }
    }
}
