using Application.Dtos;
using Application.Interfaces;
using Application.Utils;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMapsterConfiguration();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistration).Assembly));
        services.AddScoped<IEmailContentGenerator, EmailContentGenerator>();
        return services;
    }
}