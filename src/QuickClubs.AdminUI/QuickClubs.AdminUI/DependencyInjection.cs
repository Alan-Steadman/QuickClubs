using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;
using QuickClubs.AdminUI.Authentication;
using QuickClubs.AdminUI.Common.Listeners;
using QuickClubs.AdminUI.Common.Models;
using QuickClubs.AdminUI.Logging;
using QuickClubs.AdminUI.Services;

namespace QuickClubs.AdminUI;

public static class DependencyInjection
{
    public static IServiceCollection AddAdminUiClient(this IServiceCollection services)
    {
        services
            .AddApiOptions()
            .AddAuthentication()
            .AddApiServices()
            .AddNotFoundListener();

        return services;
    }

    public static IServiceCollection AddApiOptions(this IServiceCollection services)
    {
        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services)
    {
        services
            .AddScoped<ProtectedSessionStorage>()
            .AddAuthenticationCore()
            .AddTransient<AuthenticationHandler>()
            .AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddScoped<ApiService>()
            .AddScoped<IClubService, ClubService>()
            .AddTransient<RequestLoggingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

                httpClient.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
                httpClient.BaseAddress = new Uri(apiSettings.BaseUrl + "auth/");
            }).AddHttpMessageHandler<RequestLoggingHandler>();

        services.AddHttpClient<ApiService>((serviceProvider, httpClient) =>
            {
                var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

                httpClient.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
                httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
            }).AddHttpMessageHandler<AuthenticationHandler>()
            .AddHttpMessageHandler<RequestLoggingHandler>();

        return services;
    }

    public static IServiceCollection AddNotFoundListener(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor() // needed for the NotFoundListener on MainLayout.razor
            .AddScoped<NotFoundListener>();

        return services;
    }

}
