using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Persistence.Database;
using Xunit;

namespace UrlShortener.IntegrationTests.Abstractions;
public abstract class BaseIntegrationTest : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime {

    private readonly IServiceScope _scope;

    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;

    protected BaseIntegrationTest(CustomWebApplicationFactory customWebApplicationFactory) {
        _scope = customWebApplicationFactory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    public Task DisposeAsync() {
        return Task.CompletedTask;
    }

    public Task InitializeAsync() {
        return Task.CompletedTask;
    }
}
