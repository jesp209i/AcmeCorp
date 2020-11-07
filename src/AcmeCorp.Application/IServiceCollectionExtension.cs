using AcmeCorp.Application.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AcmeCorp.Application
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServiceDependencies(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<EnterCompetition>, EnterCompetitionValidator>();
            return services;
        }
    }
}
