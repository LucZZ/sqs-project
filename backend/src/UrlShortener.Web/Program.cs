using Carter;
using UrlShortener.Application;
using UrlShortener.Domain;
using UrlShortener.Infrastructure;
using UrlShortener.Persistence;
using UrlShortener.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDomainServices(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddPersistenceServices(builder.Configuration)
    .AddPresentationServices(builder.Configuration);

builder.Services.AddOpenApi();

builder.Services.AddCarter();

var app = builder.Build();

app.MapOpenApi();

app.MapCarter();

app.UseHttpsRedirection();

app.Run();