using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.MsSql;
using UrlShortener.Domain.Services;
using UrlShortener.Infrastructure.Services;
using UrlShortener.Persistence.Database;
using Xunit;

namespace UrlShortener.IntegrationTests.Abstractions;
public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime {

    public IServiceScopeFactory ServiceScopeFactory => Services.GetRequiredService<IServiceScopeFactory>();

    private readonly MsSqlContainer _msSqlContainer;

    public CustomWebApplicationFactory() {

        Environment.SetEnvironmentVariable("Database__ConnectionString", "TestValue");
        Environment.SetEnvironmentVariable("JWTOptions__JWTSecret", "supersecretjwtsecreto043tu4508guj4580tu45908ug9045ug904");
        Environment.SetEnvironmentVariable("VirusTotal__ApiKey", "adadadfwfwfw");

        _msSqlContainer = new MsSqlBuilder()
            .WithCleanUp(true)
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithPassword("P@ssw0rd1234")
            .Build();
    }

    public async  Task InitializeAsync() {
        await _msSqlContainer.StartAsync();
    }

    protected override async void ConfigureWebHost(IWebHostBuilder builder) {

        builder.ConfigureTestServices(services => {
            var DbOptions = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (DbOptions is not null) {
                services.Remove(DbOptions);
            }
            var DbOptionsConfiguration = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<ApplicationDbContext>));
            if (DbOptionsConfiguration is not null) {
                services.Remove(DbOptionsConfiguration);
            }

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(_msSqlContainer.GetConnectionString()));


            var virusTotalApi = services.SingleOrDefault(d => d.ServiceType == typeof(IVirusTotalApi));
            if (virusTotalApi is not null) {
                services.Remove(virusTotalApi);
            }

            services.AddScoped<IVirusTotalApi, VirusTotalApiFake>();

        });
    }

    async Task IAsyncLifetime.DisposeAsync() {
        await _msSqlContainer.StopAsync();
    }
}
