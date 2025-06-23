using Carter;
using UrlShortener.Application;
using UrlShortener.Domain;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Infrastructure;
using UrlShortener.Persistence;
using UrlShortener.Persistence.Database;
using UrlShortener.Presentation;
using UrlShortener.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddAndValidateOptions<DatabaseOptions>(builder.Configuration, DatabaseOptions.SectionName);
builder.Services.AddAndValidateOptions<JwtOptions>(builder.Configuration, JwtOptions.SectionName);

builder.Services.AddDomainServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddPresentationServices(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddPolicy("AllowFrontend", policy => {
        policy.WithOrigins("http://localhost:8080", "http://localhost:80") //TODO
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();

builder.Services.AddCarter();

var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapOpenApi();

app.MapCarter();

await app.MigrateDatabases();

await app.RunAsync();

public partial class Program {
    protected Program() { }
}
