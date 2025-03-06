using Application.Mapping;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DI;

public static class MapsterServiceRegistration
{
    public static IServiceCollection AddMapsterConfiguration(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        MapsterConfig.Configure(); 
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>(); 
        return services;
    }
}