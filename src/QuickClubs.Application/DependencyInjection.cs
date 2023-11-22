using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using QuickClubs.Application.Common.Behaviours;
using QuickClubs.Domain.Memberships.Services;

namespace QuickClubs.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(LoggingBehavior<,>));

            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        services.AddTransient<EndDateService>();

        return services;
    }
}