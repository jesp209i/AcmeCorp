using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;

namespace AcmeCorp.Persistence
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddPersistanceServiceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AcmeCorpContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("AcmeCorp.Persistance"));
            });
            services.AddTransient<ICompetitionRepository, CompetitionRepository>();
            return services;
        }
    }
}
