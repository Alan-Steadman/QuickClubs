using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Infrastructure.Persistence;
using QuickClubs.Infrastructure.Persistence.Repositories;

namespace QuickClubs.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString =
            configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });


        services.AddScoped<IClubRepository, ClubRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
