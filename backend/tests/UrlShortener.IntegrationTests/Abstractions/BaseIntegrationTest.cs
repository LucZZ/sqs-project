using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Domain.Entities;
using UrlShortener.Persistence.Database;
using Xunit;

namespace UrlShortener.IntegrationTests.Abstractions;
public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime {

    private readonly IServiceScope _scope;

    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;

    protected BaseIntegrationTest(CustomWebApplicationFactory customWebApplicationFactory) {
        _scope = customWebApplicationFactory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        DataSeeder = new(DbContext, _scope.ServiceProvider.GetRequiredService<UserManager<User>>());
    }

    public Task InitializeAsync() {
        return DataSeeder.SeedDataAsync();
    }
    public Task DisposeAsync() {
        return Task.CompletedTask;
    }
}
