using QuickClubs.Application;
using QuickClubs.Infrastructure;
using QuickClubs.Presentation;
using QuickClubs.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Environment)
    .AddPresentation();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        //var descriptions = app.DescribeApiVersions();
        //foreach (var desc in descriptions)
        //{
        //    var url = $"/swagger/{desc}/swagger.json";
        //    var name = desc.GroupName.ToUpperInvariant();

        //    options.SwaggerEndpoint(url, name);
        //}

        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.DisplayOperationId();
    });
    app.ApplyMigrations();
}

//app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseCustomExceptionHandlingMiddleware();

app.MapControllers();

app.Run();
