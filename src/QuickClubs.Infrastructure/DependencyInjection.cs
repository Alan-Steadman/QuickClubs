using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QuickClubs.Application.Abstractions.Authentication;
using QuickClubs.Application.Abstractions.Clock;
using QuickClubs.Application.Abstractions.Data;
using QuickClubs.Application.Abstractions.Email;
using QuickClubs.Domain.Abstractions;
using QuickClubs.Domain.Clubs.Repository;
using QuickClubs.Domain.MembershipOptions.Repository;
using QuickClubs.Domain.Memberships.Repository;
using QuickClubs.Domain.Users.Repository;
using QuickClubs.Infrastructure.Authentication;
using QuickClubs.Infrastructure.Clock;
using QuickClubs.Infrastructure.Data;
using QuickClubs.Infrastructure.Email;
using QuickClubs.Infrastructure.Persistence;
using QuickClubs.Infrastructure.Persistence.Repositories;
using System.Text;

namespace QuickClubs.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services
            .AddAuth(configuration)
            .AddPersistence(configuration)
            .AddEmail(configuration)
            .AddServices();

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
        services.AddScoped<IMembershipOptionRepository, MembershipOptionRepository>();
        services.AddScoped<IMembershipRepository, MembershipRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        return services;
    }

    public static IServiceCollection AddEmail(
    this IServiceCollection services,
    ConfigurationManager configuration)
    {
        var emailSettings = new EmailSettings();
        configuration.Bind(EmailSettings.SectionName, emailSettings);
        services.AddSingleton(Options.Create(emailSettings));

        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    private static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        return services;
    }
}
