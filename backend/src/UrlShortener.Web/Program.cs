using Carter;
using UrlShortener.Application;
using UrlShortener.Domain;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Infrastructure;
using UrlShortener.Persistence;
using UrlShortener.Presentation;
using UrlShortener.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAndValidateOptions<DatabaseOptions>(builder.Configuration, DatabaseOptions.SectionName);
builder.Services.AddAndValidateOptions<JwtOptions>(builder.Configuration, JwtOptions.SectionName);

builder.Services.AddDomainServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddPresentationServices(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddCarter();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapOpenApi();

app.MapCarter();

await app.RunAsync();