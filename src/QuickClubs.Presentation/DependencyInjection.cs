using Microsoft.Extensions.DependencyInjection;
using QuickClubs.Presentation.Common.Mapping;

namespace QuickClubs.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();

        return services;
    }
}
