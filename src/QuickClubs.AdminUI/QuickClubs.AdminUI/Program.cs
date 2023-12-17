using Microsoft.Extensions.Options;
using QuickClubs.AdminUI.Client.Pages;
using QuickClubs.AdminUI.Common.Models;
using QuickClubs.AdminUI.Components;
using QuickClubs.AdminUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddOptions<ApiSettings>()
    .BindConfiguration(ApiSettings.SectionName)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddScoped<IClubService, ClubService>();

builder.Services.AddHttpClient<IClubService, ClubService>((serviceProvider, httpClient) =>
{
    var apiSettings = serviceProvider.GetRequiredService<IOptions<ApiSettings>>().Value;

    httpClient.DefaultRequestHeaders.Add("User-Agent", apiSettings.UserAgent);
    httpClient.BaseAddress = new Uri(apiSettings.BaseUrl + "/clubs");
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
