using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UrlShortener.Domain.Base.Extensions;
using UrlShortener.Domain.Base.Options;
using UrlShortener.Infrastructure.Services;

namespace UrlShortener.Infrastructure;
public static class DependencyInjection {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {

        var virusTotalOptions = configuration.GetOptions<VirusTotalOptions>(VirusTotalOptions.SectionName);

        services.AddRefitClient<IVirusTotalApi>(new RefitSettings { ContentSerializer = new NewtonsoftJsonContentSerializer() })
            .ConfigureHttpClient(client => {
                client.BaseAddress = new Uri(virusTotalOptions.Url);
                client.DefaultRequestHeaders.Add("x-apikey", virusTotalOptions.ApiKey);
            });

        return services;
    }
}
