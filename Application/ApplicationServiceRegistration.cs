using Application.Interfaces;
using Application.Mapping;
using Application.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

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