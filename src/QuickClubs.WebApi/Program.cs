using QuickClubs.Application;
using QuickClubs.Infrastructure;
using QuickClubs.Presentation;
using QuickClubs.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var presentationAssembly = typeof(QuickClubs.Presentation.DependencyInjection).Assembly;
builder.Services.AddControllers()
    .AddApplicationPart(presentationAssembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
