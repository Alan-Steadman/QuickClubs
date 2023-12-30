using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;
using QuickClubs.AdminUI.Authentication;
using QuickClubs.AdminUI.Client.Pages;
using QuickClubs.AdminUI.Common.Listeners;
using QuickClubs.AdminUI.Common.Models;
using QuickClubs.AdminUI.Components;
using QuickClubs.AdminUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor(); // needed for the NotFoundListener on MainLayout.razor
builder.Services.AddScoped<NotFoundListener>();

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddAuthenticationCore();
builder.Services.AddTransient<AuthenticationHandler>();
builder.Services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();

builder.Services.AddOptions<ApiSettings>()
    .BindConfiguration(ApiSettings.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IClubService, ClubService>();

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

    httpClient.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
    httpClient.BaseAddress = new Uri(apiSettings.BaseUrl + "/auth");
});

builder.Services.AddScoped<ApiService>();
builder.Services.AddHttpClient<ApiService>((serviceProvider, httpClient) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

    httpClient.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
    httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
});//.AddHttpMessageHandler<AuthenticationHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
