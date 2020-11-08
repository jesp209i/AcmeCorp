using AcmeCorp.Application.Commands;
using AcmeCorp.Application.Queries;
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
            services.AddTransient<IValidator<GetSubmissions>, GetSubmissionsValidator>();
            return services;
        }
    }
}
