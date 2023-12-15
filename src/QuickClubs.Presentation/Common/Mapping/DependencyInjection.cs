using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace QuickClubs.Presentation.Common.Mapping;

internal static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        // Scan the assembly and find all the IRegistgerinterfaces and
        // register each of the different configurations:

        // Get our Mapster.TypeAdapterConfig.GlobalSettings
        var config = TypeAdapterConfig.GlobalSettings;

        // Scan the assembly for all the IRegister interfaces and Register them:
        config.Scan(Assembly.GetExecutingAssembly());

        // Then add the config to the services collection
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
