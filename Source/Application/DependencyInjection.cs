using System.Reflection;
using AutoMapper;
using FluentValidation;
using MakeMeRich.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MakeMeRich.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
